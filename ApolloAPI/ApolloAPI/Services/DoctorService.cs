using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Data;
using ApolloAPI.Data.Client.Form;
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

        internal Doctor GetDoctorByDoctorIdByDoctorId(Guid doctorId)
        {
            return doctorRepository.GetDoctorByDoctorId(doctorId);
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

        internal IEnumerable<Doctor> ListOfDoctors()
        {
            return doctorRepository.ListAllDoctors();
        }

        internal IEnumerable<Doctor> ListOfDoctors(string expertise)
        {
            return doctorRepository.ListAllDoctors().Where((d) => String.Equals(d.FieldOfExpertise, expertise, StringComparison.OrdinalIgnoreCase));
        }

        internal IEnumerable<ApolloAPI.Data.Client.Item.AppointmentGeneralItem> ListOfAppointments(Guid userId, HashSet<ApolloAPI.Data.Client.Item.AppointmentGeneralItem> appointmentList)
        {
            IEnumerable<Appointment> appointments = doctorRepository.ListAllAppointments(userId).OrderBy((a) => a.AppointmentTime);            
            foreach (Appointment appointment in appointments)
            {
                Doctor doctor = doctorRepository.GetDoctorByDoctorId(appointment.DoctorId);
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
                User user = new UserRepository().GetUserByUserId(appointment.UserId);
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
                    ReplyCount = replies.Count(),
                    Creator = new ApolloAPI.Data.Person()
                    {
                        Id = userId,
                        FullName = user.FirstName + ", " + user.LastName,
                        ProfileImage = user.ProfileImage
                    },
                    LastActive = replies.OrderBy((r) => r.RepliedAt).Last().RepliedAt
                };

                discussionList.Add(discussionGeneralItem);  
            }

            return discussionList;
        }

        internal bool CreateAppointment(AppointmentForm appointmentForm, Guid userId)
        {
            Appointment appointment = new Appointment()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
                DoctorId = appointmentForm.DoctorId,
                AppointmentTime = appointmentForm.AppointmentTime
            };

            return doctorRepository.RecordAppointment(appointment);
        }

        internal bool CreateDiscussion(DiscussionForm discussionForm, Guid userId)
        {
            Discussion discussion = new Discussion()
            {
                Id = Guid.NewGuid(),
                UserId = userId,
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
                User user = new UserRepository().GetUserByUserId(discussion.UserId);
                DiscussionGeneralItem discussionGeneralItem = new DiscussionGeneralItem()
                {
                    DiscussionId = discussion.Id,
                    Title = discussion.Title,
                    ReplyCount = replies.Count(),
                    Creator = new ApolloAPI.Data.Person()
                    {
                        Id = user.Id,
                        FullName = user.FirstName + ", " + user.LastName,
                        ProfileImage = user.ProfileImage
                    },
                    LastActive = replies.OrderBy((r) => r.RepliedAt).Last().RepliedAt
                };

                discussionList.Add(discussionGeneralItem);
            }

            return discussionList;
        }

        internal DiscussionDetailedItem GetDiscussionByDiscussionId(Guid discussionId)
        {
            Discussion discussion = doctorRepository.GetDiscussionByDiscussionId(discussionId);
            return new DiscussionDetailedItem()
            {
                DiscussionId = discussionId
            };
        }

        internal bool ReplyDiscussion(ApolloAPI.Data.Doctors.Form.ReplyForm replyForm, Guid doctorId)
        {
            Reply reply = new Reply()
            {
                Id = Guid.NewGuid(),
                DiscussionId = replyForm.DiscussionId,
                Content = replyForm.Content,
                PersonId = doctorId,
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
    }
}