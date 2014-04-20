using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ApolloAPI.Authorization;
using ApolloAPI.Data;
using ApolloAPI.Data.Calculation;
using ApolloAPI.Models;
using ApolloAPI.Services;

namespace ApolloAPI.Controllers
{
    [ApolloAuthorizeAttribute]
    [RoutePrefix("api/calculation")]
    public class CalculationController : AbstractController
    {
        private CalculationService calculationService;
        private string username;
        private bool isUser;

        public CalculationController()
        {
            calculationService = new CalculationService();
        }

        #region Testing Methods

        /// <summary>
        /// Testing Method
        /// </summary>
        /// <param name="height"></param>
        /// <param name="weight"></param>
        /// <returns></returns>
        [Route("bmi/{height}/{weight}")]
        [HttpGet]
        public ServerMessage CalculateBmi(string height, string weight)
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser)
            {
                BMIForm bmiForm = new BMIForm()
                {
                    Height = height,
                    Weight = weight,
                };

                return calculationService.CalculateBMI(bmiForm, authService.GetUserIdByUsername(username)) ?
                    new ServerMessage() { IsError = false } :
                    new ServerMessage() { IsError = true, Message = "Unable to update bmi" };
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        /// <summary>
        /// Testing Method
        /// </summary>
        /// <returns></returns>
        [Route("bmi/fetch-all/test")]
        [HttpGet]
        public IEnumerable<BMIResult> GetListOfBMIs()
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser) { return calculationService.ListOfBMIs(authService.GetUserIdByUsername(username)); }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        #endregion

        [Route("bmi")]
        [HttpPost]
        public ServerMessage CalculateBmi([FromBody] BMIForm bmiForm)
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser)
            {
                return calculationService.CalculateBMI(bmiForm, authService.GetUserIdByUsername(username)) ?
                    new ServerMessage() { IsError = false } :
                    new ServerMessage() { IsError = true, Message = "Unable to update bmi" };
            }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }

        [Route("bmi/fetch-all")]
        [HttpGet]
        public IEnumerable<BMIResult> GetListOfBMI()
        {
            username = this.RequestContext.Principal.Identity.Name;
            isUser = this.RequestContext.Principal.IsInRole("User");

            if (isUser) { return calculationService.ListOfBMIs(authService.GetUserIdByUsername(username)); }

            throw new HttpResponseException(new HttpResponseMessage(HttpStatusCode.Forbidden));
        }
    }
}
