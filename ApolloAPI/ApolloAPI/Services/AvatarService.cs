using System;
using System.Collections.Generic;
using System.Data.Spatial;
using System.Linq;
using System.Web;
using ApolloAPI.Data.Client.Form;
using ApolloAPI.Data.Client.Item;
using ApolloAPI.Data.Utility;
using ApolloAPI.Models;
using ApolloAPI.Repositories;

namespace ApolloAPI.Services
{
    public class AvatarService
    {
        private AvatarRepository avatarRepository;

        public AvatarService()
        {
            avatarRepository = new AvatarRepository();
        }

        internal AvatarProfileItem GetProfile(Guid userId)
        {
            Avatar avatar = avatarRepository.GetAvatarFromUserId(userId);
            return new AvatarProfileItem();
        }

        internal IEnumerable<LeaderboardItem> GetLeaderboard(Guid userId)
        {
            Avatar avatar = avatarRepository.GetAvatarFromUserId(userId);
            return new LinkedList<LeaderboardItem>();
        }

        internal bool ValidateForm(RunForm runForm)
        {
            object[] keys = { runForm.Coordinates };
            return keys.Any((k) => k == null) ? false : true;
        }

        internal bool UpdateRun(RunForm runForm, Guid userId)
        {
            Avatar avatar = avatarRepository.GetAvatarFromUserId(userId);
            HashSet<Route> routeList = new HashSet<Route>();
            IEnumerable<Coordinate> coordinates = runForm.Coordinates;
            Guid runId = Guid.NewGuid();
            double totalDistance = 0.0D;
            
            for (int index = 0; index < coordinates.Count(); )
            {
                Coordinate startCoordinate = coordinates.ElementAt(index++);
                Coordinate endCoordinate = coordinates.ElementAt(index++);

                Route route = new Route()
                {
                    Id = Guid.NewGuid(),
                    RunId = runId,
                    Start = DbGeography.FromText(String.Format("POINT ({1} {0})", startCoordinate.Latitude, startCoordinate.Latitude)),
                    End = DbGeography.FromText(String.Format("POINT ({1} {0})", endCoordinate.Latitude, endCoordinate.Latitude))
                };

                double? nullableDistance = route.End.Distance(route.Start);
                totalDistance = nullableDistance.HasValue ? nullableDistance.Value : 0.0D;

                routeList.Add(route);
            }

            Run run = new Run()
            {
                Id = runId,
                RanBy = avatar.Id,
                RunningTime = runForm.EndTime - runForm.StartTime,
                Routes = routeList,
            };

            return avatarRepository.SaveRun(run);
        }
    }
}