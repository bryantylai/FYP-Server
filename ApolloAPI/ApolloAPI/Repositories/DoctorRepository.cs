using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Models;

namespace ApolloAPI.Repositories
{
    public class DoctorRepository : AbstractRepository
    {
        internal IEnumerable<Doctor> ListAllDoctors()
        {
            IEnumerable<Credential> credentials = dbEntities.Credentials.Where((c) => c.Role == Role.Doctor);
            IEnumerable<Person> people = dbEntities.People.Where((d) => credentials.Any((c) => c.PersonId == d.Id));
            LinkedList<Doctor> doctors = new LinkedList<Doctor>();
            foreach (Person person in people)
            {
                doctors.AddLast(person as Doctor);
            }
            return doctors;
        }

        internal IEnumerable<Appointment> ListAllAppointments(Guid guid)
        {
            return dbEntities.Appointments.Where((a) => a.UserId == guid);
        }

        internal bool RecordAppointment(Appointment appointment)
        {
            dbEntities.Appointments.Add(appointment);
            return dbEntities.SaveChanges() == 1;
        }
    }
}