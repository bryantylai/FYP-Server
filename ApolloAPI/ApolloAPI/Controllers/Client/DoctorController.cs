using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ApolloAPI.Authorization;
using ApolloAPI.Data;
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

        #region Testing Methods

        /// <summary>
        /// Testing method
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [Route("appointment/fetch-all/test")]
        [HttpGet]
        public IEnumerable<Appointment> GetListOfAppointments()
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser) { return doctorService.ListOfAppointments(authService.GetUserIdByUsername(username)); }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        #endregion

        [Route("fetch-all")]
        [HttpGet]
        public IEnumerable<Doctor> GetListOfDoctors()
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser) { return doctorService.ListOfDoctors(); }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        [Route("appointment/fetch-all")]
        [HttpGet]
        public IEnumerable<Appointment> GetListOfAppointment()
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser) { return doctorService.ListOfAppointments(authService.GetUserIdByUsername(username)); }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        [Route("appointment/create")]
        [HttpPost]
        public ServerMessage MakeAppointmentWithDoctor([FromBody] AppointmentForm appointmentForm)
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser)
            {
                if (doctorService.ValidateForm(appointmentForm) && doctorService.CreateAppointment(appointmentForm, authService.GetUserIdByUsername(username)))
                {
                    return new ServerMessage() { IsError = false };
                }

                return new ServerMessage() { IsError = true, Message = "Unable to make appointment" };
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }
    }
}
