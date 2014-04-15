using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApolloAPI.Data
{
    public abstract class Form
    {
        public string Email { get; set; }
    }

    public abstract class AuthForm : Form
    {
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

    public class ResetForm : Form
    {
    }
}