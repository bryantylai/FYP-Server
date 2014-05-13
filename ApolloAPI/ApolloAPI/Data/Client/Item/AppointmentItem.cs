using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApolloAPI.Data.Client.Item
{
    public abstract class AppointmentItem
    {
        public Guid AppointmentId { get; set; }
    }

    public class AppointmentGeneralItem : AppointmentItem
    {
        public string DoctorName { get; set;}
        public string Reason { get; set; }
        public DateTime AppointmentTime { get; set; }
    }

    public class AppointmentDetailedItem : AppointmentItem
    {

    }
}