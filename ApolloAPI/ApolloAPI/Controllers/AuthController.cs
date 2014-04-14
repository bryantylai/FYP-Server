using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApolloAPI.Authorization;
using ApolloAPI.Models;

namespace ApolloAPI.Controllers
{
    //[ForceHttps()]
    [ApolloAuthorizeAttribute]
    [RoutePrefix("api/auth")]
    public class AuthController : ApiController
    {
        /// <summary>
        /// Testing method
        /// </summary>
        [Route("login")]
        [HttpGet]
        public Person Login()
        {
            return new Person();
        }
    }
}
