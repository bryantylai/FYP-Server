using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ApolloAPI.Data.Calculation;
using ApolloAPI.Models;
using ApolloAPI.Repositories;

namespace ApolloAPI.Services
{
    public class CalculationService
    {
        private CalculationRepository calculationRepository;
        private UserRepository userRepository;
        

        public CalculationService()
        {
            calculationRepository = new CalculationRepository();
            userRepository = new UserRepository();
        }

        internal bool CalculateBMI(BMIForm bmiForm)
        {
            double weight, height;
            User user;

            if (Double.TryParse(bmiForm.Height, out height) &&
                Double.TryParse(bmiForm.Weight, out weight) &&
                (user = userRepository.GetUserByUserId(bmiForm.UserId)) != null)
            {
                BMI bmi = new BMI()
                {
                    Id = Guid.NewGuid(),
                    Weight = Convert.ToDouble(bmiForm.Weight),
                    Height = Convert.ToDouble(bmiForm.Height),
                    RecordTime = DateTime.UtcNow,
                    UserId = bmiForm.UserId
                };

                user.BMIs.Add(bmi);

                return calculationRepository.RecordBMI(user, bmi);
            }

            return false;
        }
    }
}