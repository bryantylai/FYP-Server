using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApolloAPI.Data.Calculation
{
    public class BMIForm
    {
        public string Height { get; set; }
        public string Weight { get; set; }
        public Guid UserId { get; set; }
    }
}