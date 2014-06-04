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
            IEnumerable<Person> people = GetEveryone();
            foreach (Person person in people)
            {
                if (person.GetType() == typeof(User))
                {
                    if (person.Id == userId)
                    {
                        return person as User;
                    }
                }
            }

            return null;
        }
    }
}