using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApolloAPI.Data.Client.Item
{
    public class TrainerItem
    {
        public Guid TrainerId { get; set; }
        public string Name { get; set; }
        public string Expertise { get; set; }
        public string LocationName  { get; set; }
        public string ProfileImage{ get;set;}
        public string Phone { get; set; }
        public double DistanceFromUser { get; set; }
    }
}