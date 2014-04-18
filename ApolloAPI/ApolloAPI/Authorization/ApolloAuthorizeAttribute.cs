using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using ApolloAPI.Repositories;

namespace ApolloAPI.Authorization
{
    public class ApolloAuthorizeAttribute : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            //Case that user is authenticated using forms authentication
            //so no need to check header for basic authentication.
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                return;
            }

            var authHeader = actionContext.Request.Headers.Authorization;

            if (authHeader != null)
            {
                if (authHeader.Scheme.Equals("basic", StringComparison.OrdinalIgnoreCase) &&
                !String.IsNullOrWhiteSpace(authHeader.Parameter))
                {
                    var credArray = GetCredentials(authHeader);
                    var userName = credArray[0];
                    var password = credArray[1];

                    AuthRepository authRepository = new AuthRepository();

                    //You can use Websecurity or asp.net memebrship provider to login, for
                    //for he sake of keeping example simple, we used out own login functionality
                    if (authRepository.CheckLoginCredentials(userName, userName, password))
                    {
                        var currentPrincipal = new GenericPrincipal(new GenericIdentity(userName), authRepository.GetUserRole(userName));
                        Thread.CurrentPrincipal = currentPrincipal;
                        HttpContext.Current.User = currentPrincipal;
                        return;
                    }
                }
            }

            HandleUnauthorizedRequest(actionContext);
        }

        private string[] GetCredentials(AuthenticationHeaderValue authHeader)
        {

            //Base 64 encoded string
            var rawCred = authHeader.Parameter;
            var encoding = Encoding.GetEncoding("iso-8859-1");
            var cred = encoding.GetString(Convert.FromBase64String(rawCred));

            var credArray = cred.Split(':');

            return credArray;
        }

        private bool IsResourceOwner(string userName, HttpActionContext actionContext)
        {
            var routeData = actionContext.Request.GetRouteData();
            var resourceUserName = routeData.Values["userName"] as string;

            if (resourceUserName == userName)
            {
                return true;
            }
            return false;
        }

        private void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);

            actionContext.Response.Headers.Add("WWW-Authenticate",
            "Basic Scheme='Basic' location='http://localhost:4167/api/auth/login'");

        }
    }
}