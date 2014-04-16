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
        internal bool CheckLoginCredentials(string email, string username, string password)
        {
            return dbEntities.Credentials.Any((c) => ((c.Email == email || c.Username == username) && c.Password == password));
        }

        internal bool CheckForDuplicate(string email, string username)
        {
            return dbEntities.Credentials.Any((c) => (c.Email == email || c.Username == username));
        }

        internal bool CreateNewUser(User user, Credential credential)
        {
            dbEntities.People.Add(user);
            dbEntities.Credentials.Add(credential);
            return (dbEntities.SaveChanges() != 0);
        }

        /// <summary>
        /// testing method
        /// </summary>
        /// <returns></returns>
        internal IEnumerable<Credential> GetAllCredentials()
        {
            return dbEntities.Credentials.ToList();
        }
    }
}