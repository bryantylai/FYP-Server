using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApolloAPI.Authorization;
using ApolloAPI.Data;
using ApolloAPI.Data.Client.Form;
using ApolloAPI.Data.Client.Item;
using ApolloAPI.Data.Utility;
using ApolloAPI.Models;
using ApolloAPI.Repositories;
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
        public IEnumerable<DoctorItem> GetListOfDoctors()
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser) { return doctorService.ListOfDoctors(); }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        [Route("fetch-all")]
        [HttpPost]
        public IEnumerable<FilteredDoctorItem> GetListOfFilteredDoctors([FromBody] DoctorFilterForm doctorFilterForm)
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser) { return doctorService.ListOfDoctors(doctorFilterForm); }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        [Route("expertise")]
        [HttpGet]
        public IEnumerable<string> GetListOfExpertise()
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser) { return doctorService.ListOfExpertise(); }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        //[Route("appointment")]
        //[HttpGet]
        //public IEnumerable<AppointmentGeneralItem> GetListOfAppointment()
        //{
        //    username = this.RequestContext.Principal.Identity.Name;
        //    isUser = this.RequestContext.Principal.IsInRole("User");

        //    if (isUser) { return doctorService.ListOfAppointments(authService.GetPersonIdByUsername(username), new HashSet<AppointmentGeneralItem>()); }

        //    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        //}

        //[Route("appointment")]
        //[HttpPost]
        //public ServerMessage MakeAppointmentWithDoctor([FromBody] AppointmentForm appointmentForm)
        //{
        //    username = this.RequestContext.Principal.Identity.Name;
        //    isUser = this.RequestContext.Principal.IsInRole("User");

        //    if (isUser)
        //    {
        //        if (doctorService.ValidateForm(appointmentForm))
        //        {
        //            if (doctorService.CreateAppointment(appointmentForm, authService.GetPersonIdByUsername(username)))
        //            {
        //                return new ServerMessage() { IsError = false };
        //            }

        //            return new ServerMessage() { IsError = true, Message = "Unable to make appointment" };
        //        }

        //        return new ServerMessage() { IsError = true, Message = "There is empty fields in the Appointment form." };
        //    }

        //    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        //}

        //[Route("appointment")]
        //[HttpPut]
        //public ServerMessage RescheduleAppointmentWithDoctor([FromBody] RescheduleAppointmentForm rescheduleAppointmentForm)
        //{
        //    username = this.RequestContext.Principal.Identity.Name;
        //    isUser = this.RequestContext.Principal.IsInRole("User");

        //    if (isUser)
        //    {
        //        if (doctorService.ValidateForm(rescheduleAppointmentForm))
        //        {
        //            if (doctorService.RescheduleAppointment(rescheduleAppointmentForm, authService.GetPersonIdByUsername(username)))
        //            {
        //                return new ServerMessage() { IsError = false };
        //            }

        //            return new ServerMessage() { IsError = true, Message = "Unable to reschedule appointment" };
        //        }

        //        return new ServerMessage() { IsError = true, Message = "There is empty fields in the Appointment form." };
        //    }

        //    throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        //}


        [Route("discussion")]
        [HttpGet]
        public IEnumerable<DiscussionGeneralItem> GetListOfDiscussion()
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser) { return doctorService.ListOfDiscussions(authService.GetPersonIdByUsername(username), new HashSet<DiscussionGeneralItem>()); }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        [Route("discussion/{id}")]
        [HttpGet]
        public DiscussionDetailedItem GetDetailedDiscussion(string id)
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser)
            {
                Guid discussionId;
                if (Guid.TryParse(id, out discussionId))
                {
                    return doctorService.GetDiscussionByDiscussionId(discussionId);
                }

                throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.NotFound));
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        [Route("discussion/test")]
        [HttpGet]
        public ServerMessage CreateNewDiscussion()
        {
            DiscussionForm discussionForm = new DiscussionForm()
            {
                Title = "Sick",
                Content = "I'm sick"
            };

            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser)
            {
                if (doctorService.ValidateForm(discussionForm))
                {
                    if (doctorService.CreateDiscussion(discussionForm, authService.GetPersonIdByUsername(username)))
                    {
                        return new ServerMessage() { IsError = false };
                    }

                    return new ServerMessage() { IsError = true, Message = "Unable to create discussion" };
                }

                return new ServerMessage() { IsError = true, Message = "There is empty fields in the Discussion form." };
            }

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
                if (doctorService.ValidateForm(discussionForm))
                {
                    if (doctorService.CreateDiscussion(discussionForm, authService.GetPersonIdByUsername(username)))
                    {
                        return new ServerMessage() { IsError = false };
                    }

                    return new ServerMessage() { IsError = true, Message = "Unable to create discussion" };
                }

                return new ServerMessage() { IsError = true, Message = "There is empty fields in the Discussion form." };
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        [Route("discussion/reply")]
        [HttpPost]
        public ServerMessage ReplyToDiscussion([FromBody] ReplyForm replyForm)
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser)
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
    }
}
