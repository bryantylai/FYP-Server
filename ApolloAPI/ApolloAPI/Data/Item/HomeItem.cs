using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApolloAPI.Data.Item
{
    public class HomeItem
    {
        public string DisplayName { get; set; }
        public double Height { get; set; }
        public double Weight { get; set; }
        public double BMI { get; set; }
        public AppointmentItem Appointment { get; set; }
        public IEnumerable<LeaderboardItem> Leaderboard { get; set; }
    }
}