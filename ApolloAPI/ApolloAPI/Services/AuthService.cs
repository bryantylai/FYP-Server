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

        internal bool RegisterUser(RegistrationForm registrationForm)
        {
            if (!authRepository.CheckForDuplicate(registrationForm.Email, registrationForm.Username))
            {
                return authRepository.CreateNewUser(registrationForm.Email, registrationForm.Username, registrationForm.Password);
            }

            return false;
        }

        /// <summary>
        /// testing method
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<Models.Credential> GetAllCredentials()
        {
            return authRepository.GetAllCredentials();
        }
    }
}