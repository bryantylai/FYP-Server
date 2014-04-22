using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApolloAPI.Data.Item
{
    public class AppointmentItem
    {
        public Guid AppointmentId { get; set; }
        public string DoctorName { get; set;}
        public string Reason { get; set; }
        public DateTime AppointmentTime { get; set; }
    }
}