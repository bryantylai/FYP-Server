using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Data.Utility;

namespace ApolloAPI.Data.Client.Form
{
    public class DoctorTrainerForm
    {
        public string DoctorExpertise { get; set; }
        public string TrainerExpertise { get; set; }
        public Coordinate Location { get; set; }
    }
}