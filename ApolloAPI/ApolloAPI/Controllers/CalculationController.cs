using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using ApolloAPI.Authorization;
using ApolloAPI.Models;
using ApolloAPI.Services;

namespace ApolloAPI.Controllers
{
    [ForceHttps()]
    [ApolloAuthorizeAttribute]
    [RoutePrefix("api/calculation")]
    public class CalculationController : ApiController
    {
        private CalculationService calculationService;

        public CalculationController()
        {
            this.calculationService = new CalculationService();
        }

        [Route("bmi/{height}/{weight}")]
        [HttpGet]
        public BMI CalculateBmi(string height, string weight)
        {
            BMI bmi = new BMI() { Height = Convert.ToDouble(height) / 100, Weight = Convert.ToDouble(weight) };
            return this.calculationService.CalculateBMI(bmi);
        }

        [Route("bmi")]
        [HttpPost]
        public BMI CalculateBmi([FromBody] BMI bmi)
        {
            return this.calculationService.CalculateBMI(bmi);
        }
    }
}
