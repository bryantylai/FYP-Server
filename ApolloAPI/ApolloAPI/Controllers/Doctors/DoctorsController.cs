using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApolloAPI.Authorization;
using ApolloAPI.Data.Utility;
using ApolloAPI.Services;

namespace ApolloAPI.Controllers.Doctors
{
    //[ApolloAuthorizeAttribute]
    [RoutePrefix("api/doctor")]
    public class DoctorsController : AbstractController
    {
        private DoctorService doctorService;
        private string username;
        private bool isDoctor;

        public DoctorsController()
        {
            doctorService = new DoctorService();
        }

        [Route("register/{username}/{password}")]
        [HttpGet]
        public ServerMessage Register(string username, string password)
        {
            if (authService.RegisterDoctor(username, password))
            {
                return new ServerMessage() { IsError = false };
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }
    }
}
