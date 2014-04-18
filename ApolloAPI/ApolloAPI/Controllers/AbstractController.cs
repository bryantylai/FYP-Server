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
        //protected string username;
        //protected bool isUser;

        public AbstractController()
        {
            authService = new AuthService();
            //username = this.RequestContext.Principal.Identity.Name;
            //isUser = this.RequestContext.Principal.IsInRole("User");
        }
    }
}
