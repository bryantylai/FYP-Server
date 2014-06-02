using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Data.Utility;

namespace ApolloAPI.Data.Client.Form
{
    public class TrainerForm
    {
        public string Expertise { get; set; }
        public Coordinate Location { get; set; }
    }
}