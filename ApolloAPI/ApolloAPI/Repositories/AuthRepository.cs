using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Models;

namespace ApolloAPI.Repositories
{
    public class AuthRepository
    {

        internal static bool CheckForDuplicate(string email, string username, string password)
        {
            ApolloDatabaseEntities dbContext = new ApolloDatabaseEntities();

            var query = dbContext.Credentials.Any((c) => (c.Email == email || c.Username == username || c.Password == password));

            return query;
        }

        internal static bool CreateNewUser(string email, string username, string password)
        {
            throw new NotImplementedException();
        }

        internal static bool CheckLoginCredentials(string username, string password)
        {
            return true;
        }
    }
}