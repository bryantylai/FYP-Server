using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApolloAPI.Authorization;
using ApolloAPI.Data;
using ApolloAPI.Models;
using ApolloAPI.Services;

namespace ApolloAPI.Controllers
{
    //[ForceHttps()]
    //[ApolloAuthorizeAttribute]
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private AuthService authService;

        public AuthController()
        {
            this.authService = new AuthService();
        }

        /// <summary>
        /// Testing method for registration
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns>HttpResponseMessage</returns>
        [Route("register/{username}/{password}")]
        [HttpGet]
        public ServerMessage Register(string username, string password)
        {
            RegistrationForm regForm = new RegistrationForm()
            {
                Email = username + "@a.com",
                Username = username,
                Password = password
            };

            authService.RegisterUser(regForm);

            return new ServerMessage() { IsError = false };
        }

        /// <summary>
        /// Testing method for getting all credentials
        /// </summary>
        [Route("get-cred")]
        [HttpGet]
        public IEnumerable<Credential> GetCredentials()
        {
            return authService.GetAllCredentials();
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
