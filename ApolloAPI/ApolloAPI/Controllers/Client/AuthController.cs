using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApolloAPI.Authorization;
using ApolloAPI.Data.Form;
using ApolloAPI.Data.Utility;
using ApolloAPI.Models;
using ApolloAPI.Services;

namespace ApolloAPI.Controllers
{
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private AuthService authService;

        public AuthController()
        {
            this.authService = new AuthService();
        }

        [Route("register")]
        [HttpPost]
        public ServerMessage Register([FromBody] RegistrationForm registrationForm)
        {
            if (authService.ValidateForm(registrationForm) && authService.RegisterUser(registrationForm))
            {
                return new ServerMessage() { IsError = false };
            }

            return new ServerMessage() { IsError = true, Message = "Unable to sign up" };
        }

        [Route("login")]
        [HttpPost]
        public ServerMessage Login([FromBody] LoginForm loginForm)
        {
            if (authService.ValidateForm(loginForm) && authService.LoginUser(loginForm))
            {
                return new ServerMessage() { IsError = false };
            }

            return new ServerMessage() { IsError = true, Message = "Unable to sign in" };
        }
    }
}
