using System;
using System.Collections.Generic;
using System.Data.Spatial;
using System.Linq;
using System.Web;
using ApolloAPI.Data.Client.Form;
using ApolloAPI.Data.Client.Item;
using ApolloAPI.Models;
using ApolloAPI.Repositories;

namespace ApolloAPI.Services
{
    public class TrainerService
    {
        private TrainerRepository trainerRepository;

        public TrainerService()
        {
            trainerRepository = new TrainerRepository();
        }

        internal IEnumerable<TrainerItem> ListOfTrainers(TrainerForm trainerForm)
        {
            IEnumerable<Trainer> trainers = trainerRepository.ListAllTrainers();
            trainers = trainers.Where((d) => String.Equals(d.FieldOfExpertise, trainerForm.Expertise, StringComparison.OrdinalIgnoreCase));
            HashSet<TrainerItem> trainerItems = new HashSet<TrainerItem>();

            foreach (Trainer trainer in trainers)
            {
                Gym gym = trainerRepository.GetGymByGymId(trainer.GymId);
                Address address = trainerRepository.GetAddressByAddressId(gym.AddressId);

                DbGeography medCenterLocation = DbGeography.FromText(string.Format("POINT ({1} {0})", address.Coordinate.Latitude, address.Coordinate.Longitude));
                DbGeography userLocation = DbGeography.FromText(string.Format("POINT ({1} {0})", trainerForm.Location.Latitude, trainerForm.Location.Longitude));

                double? nullableDistance = medCenterLocation.Distance(userLocation);
                double totalDistance = nullableDistance.HasValue ? nullableDistance.Value : 0.0D;

                trainerItems.Add(new TrainerItem()
                {
                    TrainerId = trainer.Id,
                    Name = trainer.FirstName + ", " + trainer.LastName,
                    Expertise = trainer.FieldOfExpertise,
                    LocationName  = gym.Name,
                    Phone = gym.Phone,
                    DistanceFromUser = totalDistance,
                    ProfileImage = trainer.ProfileImage
                });
            }

            return trainerItems.OrderBy((t) => t.DistanceFromUser);
        }

        internal IEnumerable<TrainerItem> ListOfTrainers()
        {
            IEnumerable<Trainer> trainers = trainerRepository.ListAllTrainers();
            HashSet<TrainerItem> trainerItems = new HashSet<TrainerItem>();

            foreach (Trainer trainer in trainers)
            {
                Gym gym = trainerRepository.GetGymByGymId(trainer.GymId);

                trainerItems.Add(new TrainerItem()
                {
                    TrainerId = trainer.Id,
                    Name = trainer.FirstName + ", " + trainer.LastName,
                    Expertise = trainer.FieldOfExpertise,
                    LocationName  = gym.Name,
                    Phone = gym.Phone,
                    ProfileImage = trainer.ProfileImage
                });
            }

            return trainerItems;
        }
    }
}