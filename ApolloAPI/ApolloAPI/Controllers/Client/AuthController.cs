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
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private AuthService authService;

        public AuthController()
        {
            this.authService = new AuthService();
        }

        #region Testing Methods

        /// <summary>
        /// Testing method for registration
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        [Route("register/{username}/{password}")]
        [HttpGet]
        public ServerMessage Register(string username, string password)
        {
            RegistrationForm registrationForm = new RegistrationForm()
            {
                Email = username + "@a.com",
                Username = username,
                Password = password
            };

            if (authService.ValidateForm(registrationForm) && authService.RegisterUser(registrationForm))
            {
                return new ServerMessage() { IsError = false };
            }

            return new ServerMessage() { IsError = true, Message = "Unable to sign up" };
        }

        /// <summary>
        /// Testing method for registration
        /// </summary>
        /// <param name="username">Username</param>
        /// <param name="password">Password</param>
        /// <returns></returns>
        [Route("login/{username}/{password}")]
        [HttpGet]
        public ServerMessage Login(string username, string password)
        {
            LoginForm loginForm = new LoginForm()
            {
                Username = username,
                Password = password
            };

            if (authService.ValidateForm(loginForm) && authService.LoginUser(loginForm))
            {
                return new ServerMessage() { IsError = false };
            }

            return new ServerMessage() { IsError = true, Message = "Unable to sign in" };
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

        #endregion

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
