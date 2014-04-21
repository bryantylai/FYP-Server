﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Data;
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
            foreach (string key in keys)
            {
                if (String.IsNullOrWhiteSpace(key))
                {
                    return false;
                }
            }

            return true;
        }

        internal bool ValidateForm(LoginForm loginForm)
        {
            return (!String.IsNullOrWhiteSpace(loginForm.Email) || !String.IsNullOrWhiteSpace(loginForm.Username)) && !String.IsNullOrWhiteSpace(loginForm.Password);
        }

        internal Guid GetUserIdByUsername(string username)
        {
            return authRepository.GetUserIdByUsername(username);
        }

        internal bool RegisterUser(RegistrationForm registrationForm)
        {
            if (!authRepository.CheckForDuplicate(registrationForm.Email, registrationForm.Username))
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

            return false;
        }

        internal bool LoginUser(LoginForm loginForm)
        {
            return authRepository.CheckLoginCredentials(loginForm.Email, loginForm.Username, loginForm.Password);
        }
    }
}