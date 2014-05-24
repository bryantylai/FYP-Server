using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ApolloAPI.Models;
using ApolloAPI.Repositories;

namespace ApolloAPI.Services
{
    public class AuthService
    {
        private AuthRepository authRepository;

        public AuthService()
        {
            authRepository = new AuthRepository();
        }

        internal bool ValidateForm(ApolloAPI.Data.Client.Form.RegistrationForm registrationForm)
        {
            string[] keys = { registrationForm.Email, registrationForm.Username, registrationForm.Password };
            return keys.Any((k) => String.IsNullOrWhiteSpace(k)) ? false : true;
        }

        internal bool ValidateForm(ApolloAPI.Data.Client.Form.LoginForm loginForm)
        {
            return (!String.IsNullOrWhiteSpace(loginForm.Email) || !String.IsNullOrWhiteSpace(loginForm.Username)) && !String.IsNullOrWhiteSpace(loginForm.Password);
        }

        internal bool CheckForDuplicate(ApolloAPI.Data.Client.Form.RegistrationForm registrationForm)
        {
            return authRepository.CheckForDuplicate(registrationForm.Email, registrationForm.Username);
        }

        internal Guid GetPersonIdByUsername(string username)
        {
            return authRepository.GetPersonIdByUsername(username);
        }

        internal bool RegisterUser(ApolloAPI.Data.Client.Form.RegistrationForm registrationForm)
        {
            User user = new User()
            {
                Id = Guid.NewGuid(),
                Phone = registrationForm.Phone
            };

            Credential credential = new Credential()
            {
                Id = Guid.NewGuid(),
                Email = registrationForm.Email,
                Username = registrationForm.Username,
                Password = BCrypt.Net.BCrypt.HashPassword(registrationForm.Password),
                Role = Role.User,
                CreatedAt = DateTime.UtcNow,
                PersonId = user.Id
            };

            return authRepository.CreateNewUser(user, credential);
        }

        internal bool LoginUser(ApolloAPI.Data.Client.Form.LoginForm loginForm)
        {
            return authRepository.CheckLoginCredentials(loginForm.Email, loginForm.Username, loginForm.Password);
        }

        internal bool CheckIfNewAccount(string username)
        {
            DateTime date = authRepository.GetLastLoginDate(GetPersonIdByUsername(username));
            bool isNew = date == new DateTime() ? true : authRepository.UpdateLogin(authRepository.GetPersonIdByUsername(username), DateTime.UtcNow);
            return isNew;
        }

        #region Doctor

        internal bool RegisterDoctor(ApolloAPI.Data.Doctors.Form.RegistrationForm registrationForm)
        {
            string username = registrationForm.Username;
            string password = registrationForm.Password;

            Doctor doctor = new Doctor()
            {
                Id = Guid.NewGuid(),
                FirstName = username,
                LastName = username,
                FieldOfExpertise = username
            };

            Credential credential = new Credential()
            {
                Id = Guid.NewGuid(),
                PersonId = doctor.Id,
                Role = Role.Doctor,
                Username = username,
                Email = username + "@doctor.com",
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                CreatedAt = DateTime.UtcNow
            };

            return authRepository.CreateNewDoctor(doctor, credential);
        }

        internal bool LoginDoctor(Data.Doctors.Form.LoginForm loginForm)
        {
            return authRepository.CheckLoginCredentials(loginForm.Email, loginForm.Username, loginForm.Password);
        }

        #endregion

        #region Trainer

        internal bool RegisterTrainer(Data.Trainers.Form.RegistrationForm registrationForm)
        {
            string username = registrationForm.Username;
            string password = registrationForm.Password;

            Trainer trainer = new Trainer()
            {
                Id = Guid.NewGuid(),
                FirstName = username,
                LastName = username,
                FieldOfExpertise = username
            };

            Credential credential = new Credential()
            {
                Id = Guid.NewGuid(),
                PersonId = trainer.Id,
                Role = Role.Trainer,
                Username = username,
                Email = username + "@trainer.com",
                Password = BCrypt.Net.BCrypt.HashPassword(password),
                CreatedAt = DateTime.UtcNow
            };

            return authRepository.CreateNewTrainer(trainer, credential);
        }

        internal bool LoginTrainer(Data.Trainers.Form.LoginForm loginForm)
        {
            return authRepository.CheckLoginCredentials(loginForm.Email, loginForm.Username, loginForm.Password);
        }

        #endregion
    }
}