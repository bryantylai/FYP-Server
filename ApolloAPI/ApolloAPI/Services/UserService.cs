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
    public class UserService
    {
        private UserRepository userRepository;

        public UserService()
        {
            userRepository = new UserRepository();
        }

        internal bool ValidateForm(ProfileForm profileForm)
        {
            object[] keys = { };
            return keys.Any((k) => k == null) ? false : true;
        }

        internal HomeItem GetHomeData(Guid userId)
        {
            DoctorRepository doctorRepository = new DoctorRepository();

            User user = userRepository.GetUserByUserId(userId);
            Appointment appointment = doctorRepository.ListAllAppointments(userId)
                .Where((d) => d.AppointmentTime >= DateTime.UtcNow)
                .OrderBy((d) => d.AppointmentTime).First();
            Doctor doctor = doctorRepository.ListAllDoctors().Single((d) => d.Id == appointment.DoctorId);

            double weight = Double.Parse(user.Weight.ToString());
            double height = Double.Parse(user.Height.ToString());
            double bmi = weight / Math.Pow(height / 100, 2);

            return new HomeItem()
            {
                DisplayName = user.LastName + ", " + user.FirstName,
                Weight = Math.Round(weight, 2),
                Height = Math.Round(height, 2),
                BMI = Math.Round(bmi, 2),
                Appointment = new AppointmentItem()
                {
                    AppointmentId = appointment.Id,
                    AppointmentTime = appointment.AppointmentTime,
                    DoctorName = doctor.LastName + ", " + doctor.FirstName
                },
                Leaderboard = new LinkedList<LeaderboardItem>()
            };
        }

        internal UserProfileItem GetProfile(Guid userId)
        {
            User user = userRepository.GetUserByUserId(userId);
            return new UserProfileItem();
        }

        internal bool UpdateProfile(ProfileForm profileForm, Guid userId)
        {
            User user = userRepository.GetUserByUserId(userId);
            return userRepository.SaveUpdate();
        }
    }
}