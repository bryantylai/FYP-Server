using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Models;
using ApolloAPI.Repositories;

namespace ApolloAPI.Services
{
    public class SystemService
    {
        private PointRepository pointRepository;

        public SystemService()
        {
            pointRepository = new PointRepository();
        }

        internal void InitializeSystem()
        {
            if (!pointRepository.IsGameSystemInitialized())
            {
                HashSet<GameSystem> gameSystems = new HashSet<GameSystem>();

                for (int index = 1; index <= 100; index++)
                {
                    gameSystems.Add(new GameSystem()
                        {
                            Id = Guid.NewGuid(),
                            Level = index,
                            Points = index * 10
                        });
                }

                pointRepository.InitializeGameSystem(gameSystems);
            }

            if (!pointRepository.IsScoresheetInitialized())
            {
                HashSet<Scoresheet> scoresheets = new HashSet<Scoresheet>();

                for (int index = 1; index <= 100; index++)
                {
                    scoresheets.Add(new Scoresheet()
                        {
                            Id  = Guid.NewGuid(),
                            Points = index,
                            Distance = index * 50
                        });
                }

                pointRepository.InitializeScoresheet(scoresheets);
            }
        }
    }
}