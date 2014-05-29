using System;
using System.Collections.Generic;
using System.Data.Spatial;
using System.Linq;
using System.Web;
using ApolloAPI.Models;
using ApolloAPI.Repositories;

namespace ApolloAPI.Services
{
    public class SystemService
    {
        private PointRepository pointRepository;
        private AuthRepository authRepository;
        private DoctorRepository doctorRepository;
        private TrainerRepository trainerRepository;

        public SystemService()
        {
            pointRepository = new PointRepository();
            authRepository = new AuthRepository();
            doctorRepository = new DoctorRepository();
            trainerRepository = new TrainerRepository();
        }

        internal void InitializeGameSystem()
        {
            if (!pointRepository.IsGameSystemInitialized())
            {
                HashSet<GameSystem> gameSystems = new HashSet<GameSystem>();

                for (int index = 1; index <= 100; index++)
                {
                    gameSystems.Add(new GameSystem()
                        {
                            Id = Guid.NewGuid(),
                            Level = index,
                            Points = index * 10
                        });
                }

                pointRepository.InitializeGameSystem(gameSystems);
            }
        }

        internal void InitializeScoresheet()
        {
            if (!pointRepository.IsScoresheetInitialized())
            {
                HashSet<Scoresheet> scoresheets = new HashSet<Scoresheet>();

                for (int index = 1; index <= 100; index++)
                {
                    scoresheets.Add(new Scoresheet()
                    {
                        Id = Guid.NewGuid(),
                        Points = index,
                        Distance = index * 200
                    });
                }

                pointRepository.InitializeScoresheet(scoresheets);
            }
        }

        internal void PopulateDoctors()
        {
            if (doctorRepository.ListAllDoctors().Count() != 0) return;

            string[] mcname = { "Klinik Kathy A M", "Klinik & Surgeri Guna", "Klinik Prime Care", "Yap Dental Specialist Center" };
            string[] fname = { "Kathy", "Guna", "Prime", "Yap" };
            string[] lname = { "A M", "Guna", "Care", "Yap" };
            string[] foe = { "Medical", "Surgical", "Medical", "Dental" };
            string phone = "0123456789";
            double[] lati = { 3.079167, 3.077935, 3.081513, 3.079242 };
            double[] longi = { 101.608262, 101.608337, 101.612897, 101.613197 };

            for (int index = 0; index < fname.Length;index++)
            {
                Address address = new Address()
                {
                    Id = Guid.NewGuid(),
                    Coordinate = DbGeography.FromText(string.Format("POINT ({1} {0})", lati[index], longi[index]))
                };

                doctorRepository.CreateNewAddress(address);               

                MedicalCenter medCenter = new MedicalCenter()
                {
                    Id = Guid.NewGuid(),
                    AddressId = address.Id,
                    Name = mcname[index],
                    Phone = phone
                };

                doctorRepository.CreateNewMedicalCenter(medCenter);

                Doctor doctor = new Doctor()
                {
                    Id = Guid.NewGuid(),
                    MedicalCenterId = medCenter.Id,
                    FirstName = fname[index],
                    LastName = lname[index],
                    FieldOfExpertise = foe[index],
                    Gender = Gender.Male,
                    ProfileImage = "",
                    DateOfBirth = new DateTime(1976, 5, 9),
                    Phone = phone
                };

                Credential credential = new Credential()
                {
                    Id = Guid.NewGuid(),
                    PersonId = doctor.Id,
                    CreatedAt = DateTime.Now,
                    LastLogin = DateTime.Now,
                    Role = Role.Doctor,
                    Email = (fname[index] + lname[index]).ToLower() + "@gmail.com",
                    Username = (fname[index] + lname[index]).ToLower(),
                    Password = (fname[index] + lname[index]).ToLower()
                };

                authRepository.CreateNewDoctor(doctor, credential);
            }
        }

        internal void PopulateTrainers()
        {
            if (trainerRepository.ListAllTrainers().Count() != 0) return;

            string[] gname = { "Pure Gym Malaysia", "24 Hour Fitness Center", "Summit Climbing Gym", "V Gym"  };
            string[] fname = { "Simon", "Jeff", "Wilson", "Tony" };
            string[] lname = { "Chong", "Loy", "Chee", "Tan" };
            string[] foe = { "Muscle Building", "Stamina", "Strength", "Stamina" };
            string phone = "0123456789";
            double[] lati = { 3.082536, 3.070923, 3.057895, 3.087971,  };
            double[] longi = { 101.589047, 101.608831, 101.592523, 101.545285 };

            for (int index = 0; index < fname.Length; index++)
            {
                Address address = new Address()
                {
                    Id = Guid.NewGuid(),
                    Coordinate = DbGeography.FromText(string.Format("POINT ({1} {0})", lati[index], longi[index]))
                };

                trainerRepository.CreateNewAddress(address);

                Gym gym = new Gym()
                {
                    Id = Guid.NewGuid(),
                    AddressId = address.Id,
                    Name = gname[index],
                    Phone = phone
                };

                trainerRepository.CreateNewGym(gym);

                Trainer trainer = new Trainer()
                {
                    Id = Guid.NewGuid(),
                    GymId = gym.Id,
                    FirstName = fname[index],
                    LastName = lname[index],
                    FieldOfExpertise = foe[index],
                    Gender = Gender.Male,
                    ProfileImage = "",
                    DateOfBirth = new DateTime(1976, 5, 9),
                    Phone = phone
                };

                Credential credential = new Credential()
                {
                    Id = Guid.NewGuid(),
                    PersonId = trainer.Id,
                    CreatedAt = DateTime.Now,
                    LastLogin = DateTime.Now,
                    Role = Role.Trainer,
                    Email = (fname[index] + lname[index]).ToLower() + "@gmail.com",
                    Username = (fname[index] + lname[index]).ToLower(),
                    Password = (fname[index] + lname[index]).ToLower()
                };

                authRepository.CreateNewTrainer(trainer, credential);
            }
        }
    }
}