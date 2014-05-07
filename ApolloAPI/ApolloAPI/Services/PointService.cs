using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Models;
using ApolloAPI.Repositories;

namespace ApolloAPI.Services
{
    public class PointService
    {
        private PointRepository pointRepository;
        
        public PointService()
        {
            pointRepository = new PointRepository();
        }

        public bool InitializePointSystem()
        {
            if (!pointRepository.IsGameSystemInitialized())
            {
                HashSet<GameSystem> gameSystems = new HashSet<GameSystem>();

                GameSystem gameSystem = new GameSystem()
                {
                    Id = Guid.NewGuid(),
                    Level = 1,
                    Points = 0
                };
                gameSystems.Add(gameSystem);

                pointRepository.InitializeGameSystem(gameSystems);
            }

            if (!pointRepository.IsScoresheetInitialized())
            {

            }

            return true;
        }

        public static int Calculate(TimeSpan timeDiff, double distance)
        {

            return 0;
        }
    }
}