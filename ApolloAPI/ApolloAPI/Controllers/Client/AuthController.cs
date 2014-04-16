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
        public HttpResponseMessage Register(string username, string password)
        {
            RegistrationForm regForm = new RegistrationForm()
            {
                Email = username + "@a.com",
                Username = username,
                Password = password
            };

            authService.RegisterUser(regForm);

            return new HttpResponseMessage(HttpStatusCode.OK);
        }

        /// <summary>
        /// Testing method
        /// </summary>
        [Route("get-cred")]
        [HttpGet]
        public IEnumerable<Credential> GetCredentials()
        {
            return authService.GetAllCredentials();
        }

        [Route("register")]
        [HttpPost]
        public HttpResponseMessage Register([FromBody] RegistrationForm registrationForm)
        {
            if (authService.ValidateForm(registrationForm) && authService.RegisterUser(registrationForm))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [Route("login")]
        [HttpPost]
        public HttpResponseMessage Login([FromBody] LoginForm loginForm)
        {
            if (authService.ValidateForm(loginForm) && authService.LoginUser(loginForm))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}
