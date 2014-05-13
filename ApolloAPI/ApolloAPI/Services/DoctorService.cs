using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Data.Form;
using ApolloAPI.Data.Item;
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

        internal Doctor GetDoctorByDoctorId(Guid doctorId)
        {
            return doctorRepository.GetDoctor(doctorId);
        }

        internal IEnumerable<Doctor> ListOfDoctors()
        {
            return doctorRepository.ListAllDoctors();
        }

        internal IEnumerable<Doctor> ListOfDoctors(string expertise)
        {
            return doctorRepository.ListAllDoctors().Where((d) => String.Equals(d.FieldOfExpertise, expertise, StringComparison.OrdinalIgnoreCase));
        }

        internal IEnumerable<AppointmentGeneralItem> ListOfAppointments(Guid userId)
        {
            IEnumerable<Appointment> appointments = doctorRepository.ListAllAppointments(userId).OrderBy((a) => a.AppointmentTime);
            HashSet<AppointmentGeneralItem> appointmentList = new HashSet<AppointmentGeneralItem>();
            
            foreach (Appointment appointment in appointments)
            {
                AppointmentGeneralItem appointmentGeneralItem = new AppointmentGeneralItem()
                {

                };

                appointmentList.Add(appointmentGeneralItem);
            }

            return appointmentList;
        }

        internal IEnumerable<DiscussionGeneralItem> ListOfDiscussions(Guid userId)
        {
            IEnumerable<Discussion> discussions = doctorRepository.ListAllDiscussions();
            HashSet<DiscussionGeneralItem> discussionList = new HashSet<DiscussionGeneralItem>();
            foreach (Discussion discussion in discussions)
            {
                IEnumerable<Reply> replies = doctorRepository.GetDicussionReplies(discussion.Id);

                DiscussionGeneralItem discussionGeneralItem = new DiscussionGeneralItem()
                {
                    DiscussionId = discussion.Id,
                    Title = discussion.Title,
                    ReplyCount = replies.Count(),
                    LastActive = replies.OrderByDescending((r) => r.RepliedAt).First().RepliedAt
                };

                discussionList.Add(discussionGeneralItem);  
            }

            return discussionList;
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
    }
}