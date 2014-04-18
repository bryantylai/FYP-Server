using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using ApolloAPI.Data;
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

        internal bool CalculateBMI(BMIForm bmiForm, Guid userId)
        {
            double weight, height;

            if (Double.TryParse(bmiForm.Height, out height) &&
                Double.TryParse(bmiForm.Weight, out weight))
            {
                BMI bmi = new BMI()
                {
                    Id = Guid.NewGuid(),
                    Weight = Convert.ToDouble(bmiForm.Weight),
                    Height = Convert.ToDouble(bmiForm.Height) / 100,
                    RecordTime = DateTime.UtcNow,
                    UserId = userId
                };

                return calculationRepository.RecordBMI(bmi);
            }

            return false;
        }

        internal IEnumerable<BMIResult> ListOfBMIs(Guid guid)
        {
            IEnumerable<BMI> bmis = calculationRepository.ListAllBMIs(guid);
            LinkedList<BMIResult> bmiResults = new LinkedList<BMIResult>();
            foreach (BMI bmi in bmis)
            {
                bmiResults.AddLast(new BMIResult(bmi.Id, bmi.Height, bmi.Weight, bmi.RecordTime));
            }

            return bmiResults;
        }
    }
}