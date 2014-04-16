using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Models;

namespace ApolloAPI.Repositories
{
    public class UserRepository : AbstractRepository
    {
        public User GetUserByUserId(Guid userId)
        {
            return dbEntities.People.Single((u) => u.Id == userId) as User;
        }
    }
}