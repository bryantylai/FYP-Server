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

        public bool IsWindows(string userAgent)
        {
            return String.Equals(userAgent, "Windows", StringComparison.OrdinalIgnoreCase);
        }

        public bool IsIOS(string userAgent)
        {
            return String.Equals(userAgent, "iOS", StringComparison.OrdinalIgnoreCase);
        }
    }
}
