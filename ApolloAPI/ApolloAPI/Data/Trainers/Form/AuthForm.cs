using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApolloAPI.Data.Trainers.Form
{
    public abstract class AuthForm
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}