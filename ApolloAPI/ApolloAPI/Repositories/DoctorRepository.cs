using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using ApolloAPI.Models;

namespace ApolloAPI.Repositories
{
    public class DoctorRepository : AbstractRepository
    {
        internal Doctor GetDoctorByDoctorId(Guid doctorId)
        {
            IEnumerable<Person> people = GetEveryone();
            foreach (Person person in people)
            {
                if (person.GetType() == typeof(Doctor))
                {
                    if (person.Id == doctorId)
                    {
                        return person as Doctor;
                    }
                }
            }

            return null;
        }

        internal IEnumerable<Doctor> ListAllDoctors()
        {
            IEnumerable<Person> people = GetEveryone();
            HashSet<Doctor> doctors = new HashSet<Doctor>();
            foreach (Person person in people)
            {
                if (person.GetType() == typeof(Doctor))
                {
                    doctors.Add(person as Doctor);
                }
            }

            return doctors;
        }

        internal IEnumerable<Appointment> ListAllAppointments(Guid id)
        {
            return dbEntities.Appointments.Where((a) => a.AppointmentBy == id || a.AppointmentTo == id);
        }

        internal IEnumerable<Discussion> ListAllDiscussions()
        {
            return dbEntities.Discussions;
        }

        internal IEnumerable<Discussion> ListAllDiscussions(Guid userId)
        {
            return dbEntities.Discussions.Where((d) => d.CreatedBy == userId);
        }

        internal IEnumerable<Reply> GetDicussionReplies(Guid discussionId)
        {
            return dbEntities.Replies.Where((r) => r.DiscussionId == discussionId);
        }

        internal bool RecordAppointment(Appointment appointment)
        {
            dbEntities.Appointments.Add(appointment);
            return dbEntities.SaveChanges() == 1;
        }

        internal bool RecordDiscussion(Discussion discussion)
        {
            dbEntities.Discussions.Add(discussion);
            return dbEntities.SaveChanges() == 1;
        }

        internal Discussion GetDiscussionByDiscussionId(Guid discussionId)
        {
            return dbEntities.Discussions.Single((d) => d.Id == discussionId);
        }

        internal bool RecordDiscussionReply(Reply reply)
        {
            dbEntities.Replies.Add(reply);
            return dbEntities.SaveChanges() == 1;
        }

        internal Appointment GetAppointmentByAppointmentId(Guid appointmentId)
        {
            return dbEntities.Appointments.Single((a) => a.Id == appointmentId);
        }

        internal MedicalCenter GetMedicalCenterFromMedicalCenterId(Guid centerId)
        {
            return dbEntities.MedicalCenters.Single((m) => m.Id == centerId);
        }

        internal bool CreateNewMedicalCenter(MedicalCenter medicalCenter)
        {
            dbEntities.MedicalCenters.Add(medicalCenter);
            return dbEntities.SaveChanges() != 0;
        }
    }
}