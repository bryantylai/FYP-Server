using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApolloAPI.Authorization;
using ApolloAPI.Data.Client.Form;
using ApolloAPI.Data.Client.Item;
using ApolloAPI.Data.Utility;
using ApolloAPI.Services;

namespace ApolloAPI.Controllers.Client
{
    [ApolloAuthorizeAttribute]
    [RoutePrefix("api/client/avatar")]
    public class AvatarController : AbstractController
    {
        private AvatarService avatarService;
        private string username;
        private bool isUser;

        public AvatarController()
        {
            avatarService = new AvatarService();
        }

        [Route("profile")]
        [HttpGet]
        public AvatarProfileItem GetAvatarProfile()
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser) { return avatarService.GetProfile(authService.GetPersonIdByUsername(username)); }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        [Route("leaderboard")]
        [HttpGet]
        public IEnumerable<LeaderboardItem> GetAvatarLeaderboard()
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser) { return avatarService.GetLeaderboard(authService.GetPersonIdByUsername(username)); }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        [Route("run")]
        [HttpPost]
        public ServerMessage SubmitRunningForm([FromBody] RunForm runForm)
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser)
            {
                if (avatarService.ValidateForm(runForm) && avatarService.UpdateRun(runForm, authService.GetPersonIdByUsername(username)))
                {
                    return new ServerMessage();
                }

                return new ServerMessage() { IsError = true, Message = "Unable to update run" };
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }
    }
}
