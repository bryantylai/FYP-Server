using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApolloAPI.Authorization;
using ApolloAPI.Data.Trainers.Form;
using ApolloAPI.Data.Utility;
using ApolloAPI.Services;

namespace ApolloAPI.Controllers.Trainers
{
    [ApolloAuthorizeAttribute]
    [RoutePrefix("api/trainers")]
    public class TrainersController : AbstractController
    {
        private TrainerService trainerService;
        //private string username;
        //private bool isTrainer;

        public TrainersController()
        {
            trainerService = new TrainerService();
        }

        [Route("register")]
        [HttpPost]
        public ServerMessage Register([FromBody] RegistrationForm registrationForm)
        {
            if (authService.RegisterTrainer(registrationForm))
            {
                return new ServerMessage() { IsError = false };
            }
            else
            {
                return new ServerMessage() { IsError = true, Message = "Unable to register as trainer" };
            }
        }

        [Route("login")]
        [HttpPost]
        public ServerMessage Login([FromBody] LoginForm loginForm)
        {
            if (authService.LoginTrainer(loginForm))
            {
                return new ServerMessage() { IsError = false };
            }
            else
            {
                return new ServerMessage() { IsError = true, Message = "Unable to login as trainer" };
            }
        }
    }
}
