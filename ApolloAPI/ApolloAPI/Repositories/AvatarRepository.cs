using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Models;

namespace ApolloAPI.Repositories
{
    public class AvatarRepository : AbstractRepository
    {
        internal Avatar GetAvatarFromUserId(Guid userId)
        {
            return dbEntities.Avatars.Where((u) => u.Equals(userId)).First();
        }
    }
}