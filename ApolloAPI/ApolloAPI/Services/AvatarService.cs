using System;
using System.Collections.Generic;
using System.Data.Spatial;
using System.Globalization;
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
            IEnumerable<Run> allRuns = avatarRepository.GetRunsFromAvatarId(avatar.Id);
            HashSet<RunItem> allRunItems = new HashSet<RunItem>();
            HashSet<RunItem> monthRunItems = new HashSet<RunItem>();
            HashSet<RunItem> weekRunItems = new HashSet<RunItem>();
            HashSet<RunItem> todayRunItems = new HashSet<RunItem>();

            foreach (Run run in allRuns)
            {
                RunItem runItem = new RunItem()
                {
                    RunDate = run.StartTime.Ticks,
                    Duration = (run.EndTime - run.StartTime).Ticks,
                    Distance = run.Distance
                };

                allRunItems.Add(runItem);

                if (run.StartTime.Month == DateTime.Now.Month)
                {
                    monthRunItems.Add(runItem);
                }

                if (run.StartTime.DayOfYear + 7 > DateTime.Now.DayOfYear)
                {
                    weekRunItems.Add(runItem);
                }

                if (run.StartTime.DayOfYear == DateTime.Today.DayOfYear)
                {
                    todayRunItems.Add(runItem);
                }

            }

            GameSystem gameSystem = avatarRepository.GetGameSystem(avatar.Points);

            return new AvatarProfileItem()
            {
                Id = avatar.Id,
                Name = avatar.Name,
                ProfileImage = avatar.ProfileImage,
                Level = avatar.Level,
                Experience = (gameSystem.Points - avatar.Points) * 0.01,
                All = allRunItems,
                Month = monthRunItems,
                Week = weekRunItems,
                Day = todayRunItems
            };
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

        internal RunMessage UpdateRun(RunForm runForm, Guid userId)
        {
            Avatar avatar = avatarRepository.GetAvatarFromUserId(userId);
            Guid runId = Guid.NewGuid();

            Run run = new Run()
            {
                Id = runId,
                RanBy = avatar.Id,
                Distance = runForm.Distance,
                StartTime = new DateTime(runForm.StartTime),
                EndTime = new DateTime(runForm.EndTime)
            };

            if (avatarRepository.SaveRun(run))
            {
                Scoresheet score = avatarRepository.GetScoresheet(run.Distance);
                avatar.Points += score.Points;
                GameSystem gameSystem = avatarRepository.GetGameSystem(avatar.Points);
                avatar.Level = gameSystem.Level;

                avatarRepository.SaveUpdate();

                return new RunMessage()
                {
                    IsError = false,
                    Duration = (run.EndTime - run.StartTime).Ticks,
                    Avatar = GetProfile(userId)
                };
            }
            else
            {
                return new RunMessage()
                {
                    IsError = true,
                    Message = "Unable to update run"
                };
            }
        }
    }
}