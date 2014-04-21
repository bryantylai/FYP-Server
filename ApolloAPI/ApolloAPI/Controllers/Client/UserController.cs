using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApolloAPI.Authorization;
using ApolloAPI.Data;

namespace ApolloAPI.Controllers.Client
{
    [ApolloAuthorizeAttribute]
    [RoutePrefix("api/user")]
    public class UserController : ApiController
    {
        [Route("home")]
        [HttpGet]
        public Home GetDataForHome()
        {
            return new Home();
        }
    }
}
