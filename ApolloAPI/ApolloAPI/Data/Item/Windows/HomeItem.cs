using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Data.Item;

namespace ApolloAPI.Data.Item.Windows
{
    public class HomeItem
    {
        public string DisplayName { get; set; }
        public double Weight { get; set; }
        public double Height { get; set; }
        public double BMI { get; set; }
        public AppointmentItem Appointment { get; set; }
        public LinkedList<LeaderboardItem> LeaderboardList { get; set; }
    }
}