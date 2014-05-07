using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using ApolloAPI.Data.Form;
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

        internal bool ValidateForm(RegistrationForm registrationForm)
        {
            string[] keys = { registrationForm.Email, registrationForm.Username, registrationForm.Password };
            return keys.Any((k) => String.IsNullOrWhiteSpace(k)) ? false : true;
        }

        internal bool ValidateForm(LoginForm loginForm)
        {
            return (!String.IsNullOrWhiteSpace(loginForm.Email) || !String.IsNullOrWhiteSpace(loginForm.Username)) && !String.IsNullOrWhiteSpace(loginForm.Password);
        }

        internal bool CheckForDuplicate(RegistrationForm registrationForm)
        {
            return authRepository.CheckForDuplicate(registrationForm.Email, registrationForm.Username);
        }

        internal Guid GetPersonIdByUsername(string username)
        {
            return authRepository.GetPersonIdByUsername(username);
        }

        internal bool RegisterUser(RegistrationForm registrationForm)
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

        internal bool LoginUser(LoginForm loginForm)
        {
            return authRepository.CheckLoginCredentials(loginForm.Email, loginForm.Username, loginForm.Password);
        }
    }
}