using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Data.Utility;

namespace ApolloAPI.Data.Client.Form
{
    public class RunForm
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public IEnumerable<Coordinate> Coordinates { get; set; }
    }
}