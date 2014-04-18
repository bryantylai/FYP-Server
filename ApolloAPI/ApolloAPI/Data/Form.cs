using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApolloAPI.Data
{
    public abstract class AuthForm
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }

    public class LoginForm : AuthForm
    {
    }

    public class RegistrationForm : AuthForm
    {
        public string Phone { get; set; }
    }

    public class AppointmentForm
    {
        public Guid DoctorId { get; set; }
        public string Reason { get; set; }
        public DateTime AppointmentTime { get; set; }
    }
}