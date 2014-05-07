using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApolloAPI.Services;

namespace ApolloAPI.Controllers
{
    public abstract class AbstractController : ApiController
    {
        protected AuthService authService;

        public AbstractController()
        {
            authService = new AuthService();
        }
    }
}
