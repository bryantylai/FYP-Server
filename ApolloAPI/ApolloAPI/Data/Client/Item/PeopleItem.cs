using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApolloAPI.Data.Client.Item
{
    public class PeopleItem
    {
        public IEnumerable<TrainerItem> Trainers { get; set; }
        public IEnumerable<DoctorItem> Doctors { get; set; }
    }
}