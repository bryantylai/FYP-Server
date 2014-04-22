using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApolloAPI.Authorization;
using ApolloAPI.Data.Utility;

namespace ApolloAPI.Controllers
{
    [ApolloAuthorizeAttribute]
    [RoutePrefix("api/utility")]
    public class UtilityController : AbstractController
    {
        private string username;
        private bool isUser;

        [Route("amazons3")]
        [HttpGet]
        public AmazonS3Keys GetAmazonS3Keys()
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser) { return new AmazonS3Keys(); }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }
    }
}
