using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApolloAPI.Data.Client.Form
{

    public class ProfileForm
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string ProfileImage { get; set; }
        public string CoverImage { get; set; }
        public string DateOfBirth { get; set; }
        public string AboutMe { get; set; }
        public string Gender { get; set; }
        public string Phone { get; set; }
    }

    public class ProfileFormWindows : ProfileForm
    {
        public string Weight { get; set; }
        public string Height { get; set; }
    }

    public class BMIForm 
    {
        public string Weight { get; set; }
        public string Height { get; set; }
    }
}