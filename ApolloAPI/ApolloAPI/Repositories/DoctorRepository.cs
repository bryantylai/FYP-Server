using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Models;

namespace ApolloAPI.Repositories
{
    public class DoctorRepository : AbstractRepository
    {
        internal Doctor GetDoctor(Guid doctorId)
        {
            return dbEntities.People.Single((d) => d.Id == doctorId) as Doctor;
        }

        internal IEnumerable<Doctor> ListAllDoctors()
        {
            IEnumerable<Credential> credentials = dbEntities.Credentials.Where((c) => c.Role == Role.Doctor);
            return dbEntities.People.Where((d) => credentials.Any((c) => c.PersonId == d.Id)).Cast<Doctor>();
        }

        internal IEnumerable<Appointment> ListAllAppointments(Guid userId)
        {
            return dbEntities.Appointments.Where((a) => a.UserId == userId);
        }

        internal IEnumerable<Discussion> ListAllDiscussions()
        {
            return dbEntities.Discussions;
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
    }
}