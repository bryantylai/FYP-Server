using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApolloAPI.Data.Doctors.Item
{
    public class AppointmentItem
    {
        public Guid AppointmentId { get; set; }
        public Appointee User { get; set; }
        public string Reason { get; set; }
        public DateTime AppointmentTime { get; set; }
        public bool IsApproved { get; set; }
    }

    public class Appointee
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
    }
}