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
                GameSystem gameSystem = new GameSystem()
                {
                    Id = Guid.NewGuid(),
                    a
                };
                pointRepository.InitializeGameSystem();
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