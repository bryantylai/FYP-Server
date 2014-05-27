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
            return dbEntities.Avatars.Where((a) => a.Owner == userId).First();
        }

        internal bool SaveRun(Run run)
        {
            dbEntities.Runs.Add(run);
            return dbEntities.SaveChanges() != 0;
        }

        internal bool CreateNewAvatar(Avatar avatar)
        {
            dbEntities.Avatars.Add(avatar);
            return dbEntities.SaveChanges() != 0;
        }

        internal IEnumerable<Run> GetRunsFromAvatarId(Guid avatarId)
        {
            return dbEntities.Runs.Where((r) => r.RanBy == avatarId);
        }
    }
}