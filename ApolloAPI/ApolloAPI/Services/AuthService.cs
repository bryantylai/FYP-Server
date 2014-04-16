using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Data;
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

        /// <summary>
        /// testing method
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<Models.Credential> GetAllCredentials()
        {
            return authRepository.GetAllCredentials();
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

        internal bool RegisterUser(RegistrationForm registrationForm)
        {
            if (!authRepository.CheckForDuplicate(registrationForm.Email, registrationForm.Username))
            {
                return authRepository.CreateNewUser(registrationForm.Email, registrationForm.Username, registrationForm.Password);
            }

            return false;
        }

        internal bool LoginUser(LoginForm loginForm)
        {
            return authRepository.CheckLoginCredentials(loginForm.Email, loginForm.Username, loginForm.Password);
        }
    }
}