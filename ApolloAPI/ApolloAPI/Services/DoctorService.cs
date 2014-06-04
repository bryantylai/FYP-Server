using System;
using System.Collections.Generic;
using System.Data.Spatial;
using System.Linq;
using System.Web;
using ApolloAPI.Data;
using ApolloAPI.Data.Client.Form;
using ApolloAPI.Data.Client.Item;
using ApolloAPI.Data.Utility;
using ApolloAPI.Models;
using ApolloAPI.Repositories;

namespace ApolloAPI.Services
{
    public class DoctorService
    {
        private DoctorRepository doctorRepository;

        public DoctorService()
        {
            doctorRepository = new DoctorRepository();
        }

        internal bool ValidateForm(AppointmentForm appointmentForm)
        {
            object[] keys = { appointmentForm.DoctorId, appointmentForm.AppointmentTime };
            return keys.Any((k) => k == null) ? false : true;
        }

        internal bool ValidateForm(RescheduleAppointmentForm rescheduleAppointmentForm)
        {
            object[] keys = { rescheduleAppointmentForm.AppointmentId, rescheduleAppointmentForm.DoctorId, rescheduleAppointmentForm.AppointmentTime };
            return keys.Any((k) => k == null) ? false : true;
        }

        internal bool ValidateForm(DiscussionForm discussionForm)
        {
            object[] keys = { discussionForm.Title, discussionForm.Content };
            return keys.Any((k) => k == null) ? false : true;
        }

        internal IEnumerable<DoctorItem> ListOfDoctors()
        {
            IEnumerable<Doctor> doctors = doctorRepository.ListAllDoctors();
            HashSet<DoctorItem> doctorList = new HashSet<DoctorItem>();

            foreach (Doctor doctor in doctors)
            {
                if (doctor != null)
                {
                    MedicalCenter medCenter = doctorRepository.GetMedicalCenterFromMedicalCenterId(doctor.MedicalCenterId);
                    doctorList.Add(new DoctorItem()
                    {
                        DoctorId = doctor.Id,
                        Name = doctor.FirstName + ", " + doctor.LastName,
                        Expertise = doctor.FieldOfExpertise,
                        LocationName  = medCenter.Name,
                        Phone = medCenter.Phone,
                        ProfileImage = doctor.ProfileImage
                    });
                }
            }

            return doctorList;
        }

        internal IEnumerable<FilteredDoctorItem> ListOfDoctors(DoctorFilterForm doctorFilterForm)
        {
            IEnumerable<Doctor> doctors = doctorRepository.ListAllDoctors();
            if (!String.IsNullOrWhiteSpace(doctorFilterForm.Expertise))
            {
                doctors = doctors.Where((d) => String.Equals(d.FieldOfExpertise, doctorFilterForm.Expertise, StringComparison.OrdinalIgnoreCase));
            }            

            HashSet<FilteredDoctorItem> doctorList = new HashSet<FilteredDoctorItem>();

            foreach (Doctor doctor in doctors)
            {
                MedicalCenter medCenter = doctorRepository.GetMedicalCenterFromMedicalCenterId(doctor.MedicalCenterId);
                Address address = doctorRepository.GetAddressByAddressId(medCenter.AddressId);

                if (doctorFilterForm.Location != null)
                {
                    DbGeography medCenterLocation = DbGeography.FromText(string.Format("POINT ({1} {0})", address.Coordinate.Latitude, address.Coordinate.Longitude));
                    DbGeography userLocation = DbGeography.FromText(string.Format("POINT ({1} {0})", doctorFilterForm.Location.Latitude, doctorFilterForm.Location.Longitude));

                    double? nullableDistance = medCenterLocation.Distance(userLocation);
                    double totalDistance = nullableDistance.HasValue ? nullableDistance.Value : 0.0D;

                    doctorList.Add(new FilteredDoctorItem()
                    {
                        DoctorId = doctor.Id,
                        Name = doctor.FirstName + ", " + doctor.LastName,
                        Expertise = doctor.FieldOfExpertise,
                        DistanceFromUser = totalDistance,
                        LocationName  = medCenter.Name,
                        ProfileImage = doctor.ProfileImage,
                        Phone = medCenter.Phone
                    });
                }
                else
                {

                    doctorList.Add(new FilteredDoctorItem()
                    {
                        DoctorId = doctor.Id,
                        Name = doctor.FirstName + ", " + doctor.LastName,
                        Expertise = doctor.FieldOfExpertise,
                        LocationName = medCenter.Name,
                        ProfileImage = doctor.ProfileImage,
                        Phone = medCenter.Phone
                    });
                }
            }

            if (doctorFilterForm.Location != null)
                return doctorList.OrderBy((d) => d.DistanceFromUser);
            else 
                return doctorList;
        }

        internal IEnumerable<ApolloAPI.Data.Client.Item.AppointmentGeneralItem> ListOfAppointments(Guid userId, HashSet<ApolloAPI.Data.Client.Item.AppointmentGeneralItem> appointmentList)
        {
            IEnumerable<Appointment> appointments = doctorRepository.ListAllAppointments(userId).OrderBy((a) => a.AppointmentTime);            
            foreach (Appointment appointment in appointments)
            {
                Doctor doctor = doctorRepository.GetDoctorByDoctorId(appointment.AppointmentTo);
                ApolloAPI.Data.Client.Item.AppointmentGeneralItem appointmentGeneralItem = new ApolloAPI.Data.Client.Item.AppointmentGeneralItem()
                {
                    AppointmentId = appointment.Id,
                    Reason = appointment.Reason,
                    DoctorName = doctor.FirstName + ", " + doctor.LastName,
                    AppointmentTime = appointment.AppointmentTime
                };

                appointmentList.Add(appointmentGeneralItem);
            }

            return appointmentList;
        }

        internal IEnumerable<ApolloAPI.Data.Doctors.Item.AppointmentItem> ListOfAppointments(Guid doctorId, HashSet<ApolloAPI.Data.Doctors.Item.AppointmentItem> appointmentList)
        {
            IEnumerable<Appointment> appointments = doctorRepository.ListAllAppointments(doctorId).OrderBy((a) => a.AppointmentTime);
            foreach (Appointment appointment in appointments)
            {
                User user = new UserRepository().GetUserByUserId(appointment.AppointmentBy);
                ApolloAPI.Data.Doctors.Item.AppointmentItem appointmentGeneralItem = new ApolloAPI.Data.Doctors.Item.AppointmentItem()
                {
                    AppointmentId = appointment.Id,
                    Reason = appointment.Reason,
                    User = new ApolloAPI.Data.Doctors.Item.Appointee()
                    {
                        Id = user.Id,
                        FullName = user.FirstName + ", " + user.LastName,
                        ProfileImage = user.ProfileImage
                    },
                    IsApproved = appointment.IsApproved,
                    AppointmentTime = appointment.AppointmentTime
                };

                appointmentList.Add(appointmentGeneralItem);
            }
            
            return appointmentList;
        }

        internal IEnumerable<DiscussionGeneralItem> ListOfDiscussions(Guid userId, HashSet<DiscussionGeneralItem> discussionList)
        {
            IEnumerable<Discussion> discussions = doctorRepository.ListAllDiscussions(userId);
            foreach (Discussion discussion in discussions)
            {
                IEnumerable<Reply> replies = doctorRepository.GetDicussionReplies(discussion.Id);
                User user = new UserRepository().GetUserByUserId(userId);
                DiscussionGeneralItem discussionGeneralItem = new DiscussionGeneralItem()
                {
                    DiscussionId = discussion.Id,
                    Title = discussion.Title,
                    //ReplyCount = replies.Count(),
                    CreatorName = user.FirstName + ", " + user.LastName
                };

                discussionGeneralItem.LastActive = discussion.CreatedAt.Ticks;
                //if (replies.Count() != 0)
                //{
                //    discussionGeneralItem.ReplyCount = replies.Count();
                //    discussionGeneralItem.LastActive = replies.OrderBy((r) => r.RepliedAt).Last().RepliedAt.Ticks;
                //}
                //else
                //{
                //    discussionGeneralItem.LastActive = discussion.CreatedAt.Ticks;
                //}

                discussionList.Add(discussionGeneralItem);  
            }

            return discussionList;
        }

        internal bool CreateAppointment(AppointmentForm appointmentForm, Guid userId)
        {
            Appointment appointment = new Appointment()
            {
                Id = Guid.NewGuid(),
                AppointmentBy = userId,
                AppointmentTo = appointmentForm.DoctorId,
                AppointmentTime = appointmentForm.AppointmentTime,
                IsApproved = false,
                Reason = appointmentForm.Reason
            };

            return doctorRepository.RecordAppointment(appointment);
        }

        internal bool CreateDiscussion(DiscussionForm discussionForm, Guid userId)
        {
            Discussion discussion = new Discussion()
            {
                Id = Guid.NewGuid(),
                CreatedBy = userId,
                Title = discussionForm.Title,
                Content = discussionForm.Content,
                CreatedAt = DateTime.UtcNow
            };

            return doctorRepository.RecordDiscussion(discussion);
        }

        internal bool RescheduleAppointment(RescheduleAppointmentForm rescheduleAppointmentForm, Guid userId)
        {
            return false;
        }

        internal IEnumerable<DiscussionGeneralItem> ListOfDiscussions(HashSet<DiscussionGeneralItem> discussionList)
        {
            IEnumerable<Discussion> discussions = doctorRepository.ListAllDiscussions();
            foreach (Discussion discussion in discussions)
            {
                IEnumerable<Reply> replies = doctorRepository.GetDicussionReplies(discussion.Id);
                User user = new UserRepository().GetUserByUserId(discussion.CreatedBy);
                DiscussionGeneralItem discussionGeneralItem = new DiscussionGeneralItem()
                {
                    DiscussionId = discussion.Id,
                    Title = discussion.Title,
                    ReplyCount = replies.Count(),
                    CreatorName = user.FirstName + ", " + user.LastName,
                    LastActive = replies.OrderBy((r) => r.RepliedAt).Last().RepliedAt.Ticks
                };

                discussionList.Add(discussionGeneralItem);
            }

            return discussionList;
        }

        internal DiscussionDetailedItem GetDiscussionByDiscussionId(Guid discussionId)
        {
            UserRepository userRepository = new UserRepository();
            Discussion discussion = doctorRepository.GetDiscussionByDiscussionId(discussionId);
            IEnumerable<Reply> replies = doctorRepository.GetDicussionReplies(discussionId);
            HashSet<ReplyItem> replyItems = new HashSet<ReplyItem>();
            foreach(Reply reply in replies)
            {
                User responder = userRepository.GetUserByUserId(discussion.CreatedBy);

                replyItems.Add(new ReplyItem()
                    {
                        Content = reply.Content,
                        RepliedAt = reply.RepliedAt.Ticks,
                        ResponderName = responder.FirstName + ", " + responder.LastName
                    });
            }

            User creator = userRepository.GetUserByUserId(discussion.CreatedBy);
            return new DiscussionDetailedItem()
            {
                DiscussionId = discussionId,
                Title = discussion.Title,
                CreatorName = creator.FirstName + ", " + creator.LastName,     
                Replies = replyItems
            };
        }

        internal bool ReplyDiscussion(ApolloAPI.Data.Client.Form.ReplyForm replyForm, Guid userId)
        {
            Reply reply = new Reply()
            {
                Id = Guid.NewGuid(),
                DiscussionId = replyForm.DiscussionId,
                Content = replyForm.Content,
                RepliedBy = userId,
                RepliedAt = DateTime.UtcNow
            };

            return doctorRepository.RecordDiscussionReply(reply);
        }

        internal bool MakeApproval(Guid appointmentId)
        {
            Appointment appointment = doctorRepository.GetAppointmentByAppointmentId(appointmentId);
            appointment.IsApproved = !appointment.IsApproved;
            return doctorRepository.SaveUpdate();
        }

        internal IEnumerable<string> ListOfExpertise()
        {
            IEnumerable<Doctor> doctors = doctorRepository.ListAllDoctors();
            HashSet<string> expertise = new HashSet<string>();
            foreach (Doctor doctor in doctors)
            {
                if (!expertise.Contains(doctor.FieldOfExpertise)) expertise.Add(doctor.FieldOfExpertise);
            }

            return expertise;
        }
    }
}