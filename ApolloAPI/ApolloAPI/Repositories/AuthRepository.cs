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

        internal bool CreateNewUser(string email, string username, string password)
        {
            User user = new User()
            {
                Id = Guid.NewGuid()
            };
            dbEntities.People.Add(user);

            Credential credential = new Credential()
            {
                Id = Guid.NewGuid(),
                Email = email,
                Username = username,
                Password = password,
                Role = Role.User,
                CreatedAt = DateTime.UtcNow,
                Person = user
            };

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