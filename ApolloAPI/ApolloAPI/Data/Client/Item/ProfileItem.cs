using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApolloAPI.Data.Client.Item
{
    public abstract class ProfileItem
    {
        public Guid Id { get; set; }
        public string ProfileImage { get; set; }
    }

    public class UserProfileItem : ProfileItem
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string CoverImage { get; set; }
        public long DateOfBirth { get; set; }
        public string AboutMe { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
    }

    public class AvatarProfileItem : ProfileItem
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public double Experience { get; set; }
        public IEnumerable<RunItem> All { get; set; }
        public IEnumerable<RunItem> Month { get; set; }
        public IEnumerable<RunItem> Week { get; set; }
        public IEnumerable<RunItem> Day { get; set; }
    }

    public class AvatarProfileItemWindows
    {
        public string Name { get; set; }
        public int Level { get; set; }
        public double Experience { get; set; }
        public long Duration { get; set; }
        public double Distance { get; set; }
    }

    public class AvatarHistoryItemWindows
    {
        public IEnumerable<RunItem> Year { get; set; }
        public IEnumerable<RunItem> Month { get; set; }
        public IEnumerable<RunItem> Week { get; set; }
        public IEnumerable<RunItem> Day { get; set; }
    }

    public class RunItem
    {
        public long RunDate { get; set; }
        public double Distance { get; set; }
        public long Duration { get; set; }
    }
}