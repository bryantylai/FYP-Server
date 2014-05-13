using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApolloAPI.Authorization;
using ApolloAPI.Data.Doctors.Form;
using ApolloAPI.Data.Doctors.Item;
using ApolloAPI.Data.Utility;
using ApolloAPI.Services;

namespace ApolloAPI.Controllers.Doctors
{
    //[ApolloAuthorizeAttribute]
    [RoutePrefix("api/doctors")]
    public class DoctorsController : AbstractController
    {
        private DoctorService doctorService;
        private string username;
        private bool isDoctor;

        public DoctorsController()
        {
            doctorService = new DoctorService();
        }

        [Route("register")]
        [HttpPost]
        public ServerMessage Register([FromBody] RegistrationForm registrationForm)
        {
            if (authService.RegisterDoctor(registrationForm))
            {
                return new ServerMessage() { IsError = false };
            }
            else
            {
                return new ServerMessage() { IsError = true, Message = "Unable to register as doctor" };
            }
        }

        [Route("login")]
        [HttpPost]
        public ServerMessage Login([FromBody] LoginForm loginForm)
        {
            if (authService.LoginDoctor(loginForm))
            {
                return new ServerMessage() { IsError = false };
            }
            else
            {
                return new ServerMessage() { IsError = true, Message = "Unable to login as doctor" };
            }
        }

        [Route("discussion/fetch-all")]
        [HttpGet]
        public IEnumerable<DiscussionGeneralItem> GetListOfDiscussion()
        {
            username = this.RequestContext.Principal.Identity.Name;
            isDoctor = this.RequestContext.Principal.IsInRole("Doctor");

            if (isDoctor) { return doctorService.ListOfDiscussions(authService.GetPersonIdByUsername(username), new HashSet<DiscussionGeneralItem>()); }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        [Route("discussion/{id}")]
        [HttpGet]
        public DiscussionDetailedItem GetDiscussion(string id)
        {
            isDoctor = this.RequestContext.Principal.IsInRole("Doctor");

            if (isDoctor)
            {
                Guid discussionId = Guid.Parse(id);
                return doctorService.GetDiscussion(discussionId);
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        [Route("discussion")]
        [HttpPost]
        public ServerMessage ReplyToDiscussion([FromBody] ReplyForm replyForm)
        {
            username = this.RequestContext.Principal.Identity.Name;
            isDoctor = this.RequestContext.Principal.IsInRole("Doctor");

            if (isDoctor) 
            {
                if (doctorService.ReplyDiscussion(replyForm, authService.GetPersonIdByUsername(username)))
                {
                    return new ServerMessage() { IsError = false };
                }
                else
                {
                    return new ServerMessage() { IsError = true, Message = "Unable to reply to discussion" };
                }
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        [Route("appointment/fetch-all")]
        [HttpGet]
        public IEnumerable<AppointmentItem> GetAppointments()
        {
            username = this.RequestContext.Principal.Identity.Name;
            isDoctor = this.RequestContext.Principal.IsInRole("Doctor");

            if (isDoctor) { return doctorService.ListOfAppointments(authService.GetPersonIdByUsername(username), new HashSet<AppointmentItem>()); }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }
    }
}
