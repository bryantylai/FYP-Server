using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApolloAPI.Authorization;
using ApolloAPI.Data.Client.Form;
using ApolloAPI.Data.Client.Item;
using ApolloAPI.Services;

namespace ApolloAPI.Controllers.Client
{
    [ApolloAuthorizeAttribute]
    [RoutePrefix("api/trainer")]
    public class TrainerController : AbstractController
    {
        private TrainerService trainerService;
        private string username;
        private bool isUser;

        public TrainerController()
        {
            trainerService = new TrainerService();
        }

        [Route("fetch-all")]
        [HttpPost]
        public IEnumerable<TrainerItem> GetListOfTrainers(TrainerForm trainerForm)
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser) { return trainerService.ListOfTrainers(trainerForm); }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }
    }
}
