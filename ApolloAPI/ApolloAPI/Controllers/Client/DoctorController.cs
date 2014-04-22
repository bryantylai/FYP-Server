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
using ApolloAPI.Models;
using ApolloAPI.Services;

namespace ApolloAPI.Controllers.Client
{
    [ApolloAuthorizeAttribute]
    [RoutePrefix("api/doctor")]
    public class DoctorController : AbstractController
    {
        private DoctorService doctorService;
        private string username;
        private bool isUser;
        
        public DoctorController()
        {
            doctorService = new DoctorService();
        }

        [Route("fetch-all")]
        [HttpGet]
        public IEnumerable<Doctor> GetListOfDoctors()
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser) { return doctorService.ListOfDoctors(); }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        [Route("appointment")]
        [HttpGet]
        public IEnumerable<Appointment> GetListOfAppointment()
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser) { return doctorService.ListOfAppointments(authService.GetPersonIdByUsername(username)); }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        [Route("appointment")]
        [HttpPost]
        public ServerMessage MakeAppointmentWithDoctor([FromBody] AppointmentForm appointmentForm)
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser)
            {
                if (doctorService.ValidateForm(appointmentForm) && doctorService.CreateAppointment(appointmentForm, authService.GetPersonIdByUsername(username)))
                {
                    return new ServerMessage() { IsError = false };
                }

                return new ServerMessage() { IsError = true, Message = "Unable to make appointment" };
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        [Route("discussion")]
        [HttpGet]
        public IEnumerable<DiscussionItem> GetListOfDiscussion()
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser) { return doctorService.ListOfDiscussions(authService.GetPersonIdByUsername(username)); }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        [Route("discussion")]
        [HttpPost]
        public ServerMessage CreateNewDiscussion([FromBody] DiscussionForm discussionForm)
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser)
            {
                if (doctorService.ValidateForm(discussionForm) && doctorService.CreateDiscussion(discussionForm, authService.GetPersonIdByUsername(username)))
                {
                    return new ServerMessage() { IsError = false };
                }

                return new ServerMessage() { IsError = true, Message = "Unable to create discussion" };
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }
    }
}
