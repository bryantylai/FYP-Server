using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Models;

namespace ApolloAPI.Repositories
{
    public class PointRepository : AbstractRepository
    {
        public bool IsGameSystemInitialized()
        {
            return dbEntities.GameSystems.Count() != 0;
        }
        public bool IsScoresheetInitialized()
        {
            return dbEntities.Scoresheets.Count() != 0;
        }

        public bool InitializeGameSystem(IEnumerable<GameSystem> gameSystems)
        {
            gameSystems.All((g) => dbEntities.GameSystems.Add(g) != null);
            return dbEntities.SaveChanges() != 0;
        }

        public bool InitializeScoresheet(IEnumerable<Scoresheet> scoresheets)
        {
            scoresheets.All((s) => dbEntities.Scoresheets.Add(s) != null);
            return dbEntities.SaveChanges() != 0;
        }
    }
}