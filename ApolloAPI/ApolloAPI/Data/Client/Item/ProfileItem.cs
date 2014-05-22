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
        public string DateOfBirth { get; set; }
        public string AboutMe { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
    }

    public class AvatarProfileItem : ProfileItem
    {
    }
}