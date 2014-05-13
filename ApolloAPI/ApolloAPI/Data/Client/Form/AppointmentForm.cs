using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApolloAPI.Data.Client.Form
{
    public class AppointmentForm
    {
        public Guid DoctorId { get; set; }
        public string Reason { get; set; }
        public DateTime AppointmentTime { get; set; }
    }

    public class RescheduleAppointmentForm : AppointmentForm
    {
        public Guid AppointmentId { get; set; }
    }
}