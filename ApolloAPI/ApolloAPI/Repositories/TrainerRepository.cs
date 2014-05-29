using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Models;

namespace ApolloAPI.Repositories
{
    public class TrainerRepository : AbstractRepository
    {
        internal Doctor GetTrainerByTrainerId(Guid trainerId)
        {
            IEnumerable<Person> people = GetEveryone();
            foreach (Person person in people)
            {
                if (person.GetType() == typeof(Trainer))
                {
                    if (person.Id == trainerId)
                    {
                        return person as Doctor;
                    }
                }
            }

            return null;
        }

        internal IEnumerable<Trainer> ListAllTrainers()
        {
            IEnumerable<Person> people = GetEveryone();
            HashSet<Trainer> trainers = new HashSet<Trainer>();
            foreach (Person person in people)
            {
                if (person.GetType() == typeof(Trainer))
                {
                    trainers.Add(person as Trainer);
                }
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