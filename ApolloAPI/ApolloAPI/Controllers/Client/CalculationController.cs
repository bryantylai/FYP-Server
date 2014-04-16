using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ApolloAPI.Authorization;
using ApolloAPI.Data.Calculation;
using ApolloAPI.Models;
using ApolloAPI.Services;

namespace ApolloAPI.Controllers
{
    //[ForceHttps()]
    [ApolloAuthorizeAttribute]
    [RoutePrefix("api/calculation")]
    public class CalculationController : ApiController
    {
        private CalculationService calculationService;

        public CalculationController()
        {
            this.calculationService = new CalculationService();
        }

        [Route("bmi/{height}/{weight}/{userId}")]
        [HttpGet]
        public HttpResponseMessage CalculateBmi(string height, string weight, string userId)
        {
            BMIForm bmiForm = new BMIForm()
            {
                Height = height,
                Weight = weight,
                UserId = Guid.Parse(userId)
            };

            return calculationService.CalculateBMI(bmiForm) ? new HttpResponseMessage(HttpStatusCode.OK) : new HttpResponseMessage(HttpStatusCode.BadRequest);
        }

        [Route("bmi")]
        [HttpPost]
        public HttpResponseMessage CalculateBmi([FromBody] BMIForm bmiForm)
        {
            return calculationService.CalculateBMI(bmiForm) ? new HttpResponseMessage(HttpStatusCode.OK) : new HttpResponseMessage(HttpStatusCode.BadRequest);
        }
    }
}
