using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Data.Form;
using ApolloAPI.Data.Item;
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
            object[] keys = { runForm.StartTime, runForm.EndTime, runForm.Distance };
            return keys.Any((k) => k == null) ? false : true;
        }

        internal bool UpdateRun(RunForm runForm, Guid userId)
        {
            Avatar avatar = avatarRepository.GetAvatarFromUserId(userId);
            TimeSpan timeDiff = runForm.EndTime - runForm.StartTime;
            int point = PointService.Calculate(timeDiff, runForm.Distance);

            Run run = new Run()
            {
                Id = Guid.NewGuid(),
                AvatarId = avatar.Id,
                Distance = runForm.Distance,
                RunningTime = timeDiff,
                Point = point
            };

            return true;
        }
    }
}