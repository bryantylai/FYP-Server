using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApolloAPI.Authorization;
using ApolloAPI.Data;

namespace ApolloAPI.Controllers
{
    [ApolloAuthorizeAttribute]
    [RoutePrefix("api/utility")]
    public class UtilityController : ApiController
    {
        [Route("amazons3")]
        [HttpGet]
        public AmazonS3Keys GetAmazonS3Keys()
        {
            return new AmazonS3Keys();
        }
    }
}
