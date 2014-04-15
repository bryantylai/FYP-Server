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
    [ForceHttps()]
    [ApolloAuthorizeAttribute]
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        private AuthService authService;

        public AuthController()
        {
            this.authService = new AuthService();
        }

        /// <summary>
        /// Testing method
        /// </summary>
        [Route("login")]
        [HttpGet]
        public Person Login()
        {
            return new Person();
        }

        [Route("register")]
        [HttpPost]
        public HttpResponseMessage Register([FromBody] RegistrationForm registrationForm)
        {
            if (authService.ValidateForm(registrationForm))
            {
                return new HttpResponseMessage(HttpStatusCode.OK);
            }

            authService.RegisterUser(registrationForm);
            return new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [Route("login")]
        [HttpPost]
        public void Login([FromBody] LoginForm loginForm)
        {

        }
    }
}
