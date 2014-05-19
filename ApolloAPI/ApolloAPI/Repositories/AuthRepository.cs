using System;
using System.Collections.Generic;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using ApolloAPI.Models;

namespace ApolloAPI.Repositories
{
    public class AuthRepository : AbstractRepository
    {
        internal Guid GetPersonIdByUsername(string username)
        {
            Credential credential = dbEntities.Credentials.Where((c) => c.Username == username).First();
            return credential.PersonId;
        }

        internal bool CheckLoginCredentials(string email, string username, string password)
        {
            IEnumerable<Credential> credentials = dbEntities.Credentials.Where((c) => (c.Email == email || c.Username == username));
            if (credentials.Count() != 1) return false;
            return BCrypt.Net.BCrypt.Verify(password, credentials.First().Password);
        }

        internal bool CheckForDuplicate(string email, string username)
        {
            return dbEntities.Credentials.Any((c) => (c.Email == email || c.Username == username));
        }

        internal DateTime GetLastLoginDate(Guid personId)
        {
            return dbEntities.Credentials.Single((c) => c.PersonId == personId).LastLogin;
        }

        internal string[] GetUserRole(string userName)
        {
            Credential credential = dbEntities.Credentials.Where((c) => c.Username == userName).First();
            switch (credential.Role)
            {
                case Role.Admin:
                    return new string[] { "Admin" };
                case Role.Doctor:
                    return new string[] { "Doctor" };
                case Role.Trainer:
                    return new string[] { "Trainer" };
                case Role.User:
                    return new string[] { "User" };
            }

            return null;
        }

        #region User

        internal bool CreateNewUser(User user, Credential credential)
        {
            dbEntities.People.Add(user);
            dbEntities.Credentials.Add(credential);
            return dbEntities.SaveChanges() == 2;
        }

        #endregion

        #region Doctor

        internal bool CreateNewDoctor(Doctor doctor, Credential credential)
        {
            dbEntities.People.Add(doctor);
            dbEntities.Credentials.Add(credential);
            return dbEntities.SaveChanges() == 2;
        }

        #endregion

        #region Trainer

        internal bool CreateNewTrainer(Trainer trainer, Credential credential)
        {
            dbEntities.People.Add(trainer);
            dbEntities.Credentials.Add(credential);
            return dbEntities.SaveChanges() == 2;
        }

        #endregion
    }
}