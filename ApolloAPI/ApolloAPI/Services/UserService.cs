﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Data.Client.Form;
using ApolloAPI.Data.Client.Item;
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

        internal ApolloAPI.Data.Client.Item.Windows.HomeItem GetHomeData(Guid userId, ApolloAPI.Data.Client.Item.Windows.HomeItem homeItem)
        {
            DoctorRepository doctorRepository = new DoctorRepository();

            User user = userRepository.GetUserByUserId(userId);
            IEnumerable<Appointment> appointments = doctorRepository.ListAllAppointments(userId);
            Appointment appointment = null;
            Doctor doctor = null;
            if (appointments.Count() > 0)
            {
                appointment = doctorRepository.ListAllAppointments(userId)
                    .Where((d) => d.AppointmentTime >= DateTime.UtcNow)
                    .OrderBy((d) => d.AppointmentTime).First();
                doctor = doctorRepository.ListAllDoctors().Single((d) => d.Id == appointment.AppointmentTo);
            }

            double weight = (user.Weight != null) ? weight = Double.Parse(user.Weight.ToString()) : 0.00D;
            double height = (user.Height != null) ? height = Double.Parse(user.Height.ToString()) : 0.00D;
            double bmi = (user.Weight != null && user.Height != null) ? weight / Math.Pow(height / 100, 2) : 0.00D;

            homeItem.DisplayName = user.LastName + (user.LastName != null ? ", " : "") + user.FirstName;
            homeItem.Weight = Math.Round(weight, 2);
            homeItem.Height = Math.Round(height, 2);
            homeItem.BMI = Math.Round(bmi, 2);
            if (appointment != null)
            {
                homeItem.Appointment = new AppointmentGeneralItem()
                {
                    AppointmentId = appointment.Id,
                    AppointmentTime = appointment.AppointmentTime,
                    DoctorName = doctor.LastName + ", " + doctor.FirstName
                };
            }
            homeItem.LeaderboardList = new LinkedList<LeaderboardItem>();

            return homeItem;
        }

        internal ApolloAPI.Data.Client.Item.iOS.HomeItem GetHomeData(Guid userId, ApolloAPI.Data.Client.Item.iOS.HomeItem homeItem)
        {
            DoctorRepository doctorRepository = new DoctorRepository();

            User user = userRepository.GetUserByUserId(userId);
            Appointment appointment = doctorRepository.ListAllAppointments(userId)
                .Where((d) => d.AppointmentTime >= DateTime.UtcNow)
                .OrderBy((d) => d.AppointmentTime).First();
            Doctor doctor = doctorRepository.ListAllDoctors().Single((d) => d.Id == appointment.AppointmentTo);            

            double weight = Double.Parse(user.Weight.ToString());
            double height = Double.Parse(user.Height.ToString());
            double bmi = weight / Math.Pow(height / 100, 2);

            homeItem.DisplayName = user.LastName + ", " + user.FirstName;
            homeItem.Weight = Math.Round(weight, 2);
            homeItem.Height = Math.Round(height, 2);
            homeItem.BMI = Math.Round(bmi, 2);
            homeItem.Appointment = new AppointmentGeneralItem()
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