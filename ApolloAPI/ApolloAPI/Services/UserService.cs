using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Data.Form;
using ApolloAPI.Data.Item;
using ApolloAPI.Data.Item.Windows;
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

        internal Data.Item.Windows.HomeItem GetHomeData(Guid userId, Data.Item.Windows.HomeItem homeItem)
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

            homeItem.DisplayName = user.LastName + ", " + user.FirstName;
            homeItem.Weight = Math.Round(weight, 2);
            homeItem.Height = Math.Round(height, 2);
            homeItem.BMI = Math.Round(bmi, 2);
            homeItem.Appointment = new AppointmentItem()
            {
                AppointmentId = appointment.Id,
                AppointmentTime = appointment.AppointmentTime,
                DoctorName = doctor.LastName + ", " + doctor.FirstName
            };
            homeItem.LeaderboardList = new LinkedList<LeaderboardItem>();

            return homeItem;
        }

        internal Data.Item.iOS.HomeItem GetHomeData(Guid userId, Data.Item.iOS.HomeItem homeItem)
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

            homeItem.DisplayName = user.LastName + ", " + user.FirstName;
            homeItem.Weight = Math.Round(weight, 2);
            homeItem.Height = Math.Round(height, 2);
            homeItem.BMI = Math.Round(bmi, 2);
            homeItem.Appointment = new AppointmentItem()
            {
                AppointmentId = appointment.Id,
                AppointmentTime = appointment.AppointmentTime,
                DoctorName = doctor.LastName + ", " + doctor.FirstName
            };
            homeItem.LeaderboardList = new LinkedList<LeaderboardItem>();

            return homeItem;
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