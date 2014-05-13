using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApolloAPI.Authorization;
using ApolloAPI.Services;

namespace ApolloAPI.Controllers.Admin
{
    [ApolloAuthorizeAttribute]
    [RoutePrefix("api/admin")]
    public class SystemController : AbstractController
    {
        private string username;
        private bool isAdmin;

        [Route("system-initialize")]
        [HttpGet]
        public void InitializeSystem()
        {
            username = this.RequestContext.Principal.Identity.Name;
            isAdmin = this.RequestContext.Principal.IsInRole("Admin");

            if (isAdmin) 
            {
                
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }
    }
}
