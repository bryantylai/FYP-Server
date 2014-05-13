using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApolloAPI.Authorization;
using ApolloAPI.Data.Form;
using ApolloAPI.Data.Item;
using ApolloAPI.Data.Utility;
using ApolloAPI.Services;

namespace ApolloAPI.Controllers.Client
{
    [ApolloAuthorizeAttribute]
    [RoutePrefix("api/client/user")]
    public class UserController : AbstractController
    {
        private UserService userService;
        private string username;
        private bool isUser;

        public UserController()
        {
            userService = new UserService();
        }

        [Route("windows/home")]
        [HttpGet]
        public Data.Item.Windows.HomeItem GetWindowsDataForHome()
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser) { return userService.GetHomeData(authService.GetPersonIdByUsername(username), new Data.Item.Windows.HomeItem()); }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        [Route("ios/home")]
        [HttpGet]
        public Data.Item.iOS.HomeItem GetIOSDataForHome()
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser) { return userService.GetHomeData(authService.GetPersonIdByUsername(username), new Data.Item.iOS.HomeItem()); }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        [Route("profile")]
        [HttpGet]
        public UserProfileItem GetUserProfile()
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser) { return userService.GetProfile(authService.GetPersonIdByUsername(username)); }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        [Route("profile")]
        [HttpPut]
        public ServerMessage UpdateUserProfile([FromBody] ProfileForm profileForm)
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser)
            {
                if (userService.ValidateForm(profileForm))
                {
                    if (userService.UpdateProfile(profileForm, authService.GetPersonIdByUsername(username)))
                    {
                        return new ServerMessage() { IsError = false };
                    }

                    return new ServerMessage() { IsError = true, Message = "Unable to update profile" };
                }

                return new ServerMessage() { IsError = true, Message = "There is empty fields in the Profile form." };
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }
    }
}