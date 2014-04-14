using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ApolloAPI.Models;

namespace ApolloAPI.Services
{
    public class CalculationService
    {
        internal BMI CalculateBMI(BMI bmi)
        {
            bmi.RecordTime = DateTime.Now;
            return bmi;
        }
    }
}