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
    [RoutePrefix("api/professional")]
    public class DoctorTrainerController : AbstractController
    {
        private TrainerService trainerService;
        private DoctorService doctorService;
        private string username;
        private bool isUser;

        public DoctorTrainerController()
        {
            trainerService = new TrainerService();
            doctorService = new DoctorService();
        }

        [Route("fetch-all")]
        [HttpPost]
        public PeopleItem GetListOfDoctorsAndTrainers([FromBody] DoctorTrainerForm doctorTrainerForm)
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser)
            {
                PeopleItem peopleItem = new PeopleItem();
                DoctorFilterForm doctorForm = new DoctorFilterForm()
                    {
                        Expertise = doctorTrainerForm.DoctorExpertise,
                        Location = doctorTrainerForm.Location
                    };
                peopleItem.Doctors = doctorService.ListOfDoctors(doctorForm);

                TrainerForm trainerForm = new TrainerForm()
                {
                    Expertise = doctorTrainerForm.TrainerExpertise,
                    Location = doctorTrainerForm.Location
                };
                peopleItem.Trainers = trainerService.ListOfTrainers(trainerForm);

                return peopleItem;
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }
    }
}
