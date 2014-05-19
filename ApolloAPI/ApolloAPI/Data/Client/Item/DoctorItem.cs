using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApolloAPI.Data.Client.Item
{
    public class DoctorItem
    {
        public Guid DoctorId { get; set; }
        public string Name { get; set; }
        public string Expertise { get; set; }
        public string CenterName { get; set; }
        public string Phone { get; set; }
    }

    public class FilteredDoctorItem : DoctorItem
    {
        public double DistanceFromUser { get; set; }
    }
}