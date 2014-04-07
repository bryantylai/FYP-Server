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
            bmi.Result = bmi.Weight / Math.Pow(bmi.Height, 2);
            return bmi;
        }
    }
}