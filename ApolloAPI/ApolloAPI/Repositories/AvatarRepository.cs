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
            return dbEntities.Runs.Where((r) => r.RanBy == avatarId).OrderBy((r) => r.StartTime);
        }

        internal Scoresheet GetScoresheet(double distance)
        {
            return dbEntities.Scoresheets.Where((s) => s.Distance <= distance).OrderByDescending((s) => s.Distance).First();
        }

        internal GameSystem GetGameSystem(int points)
        {
            return dbEntities.GameSystems.Where((g) => g.Points >= points).OrderBy((g) => g.Points).First();
        }

        //internal GameSystem GetNextGameSystem(int points)
        //{
        //    IEnumerable<GameSystem> gameSystems = dbEntities.GameSystems;
        //    gameSystems = dbEntities.GameSystems.Where((g) => g.Points > points);
        //    return gameSystems.OrderBy((g) => g.Points).First();
        //}

        internal IEnumerable<Avatar> GetAvatarsWithinPointRange(int points)
        {
            return dbEntities.Avatars.Where((a) => (a.Points + 1000) > points).Union(dbEntities.Avatars.Where((a) => (a.Points - 1000) < points));
        }
    }
}