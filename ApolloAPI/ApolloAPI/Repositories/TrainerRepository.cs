using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Models;

namespace ApolloAPI.Repositories
{
    public class TrainerRepository : AbstractRepository
    {
        internal IEnumerable<Trainer> ListAllTrainers()
        {
            IEnumerable<Credential> credentials = dbEntities.Credentials.Where((c) => c.Role == Role.Trainer);
            HashSet<Trainer> trainers = new HashSet<Trainer>();
            foreach (Credential credential in credentials)
            {
                Person person = dbEntities.People.Single((d) => d.Id == credential.PersonId);
                trainers.Add(person as Trainer);
            }

            return trainers;
        }

        internal Gym GetGymByGymId(Guid gymId)
        {
            return dbEntities.Gyms.Single((g) => g.Id == gymId);
        }

        internal bool CreateNewGym(Gym gym)
        {
            dbEntities.Gyms.Add(gym);
            return dbEntities.SaveChanges() != 0;
        }
    }
}