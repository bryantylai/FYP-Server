using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Models;

namespace ApolloAPI.Repositories
{
    public class CalculationRepository : AbstractRepository
    {
        internal bool RecordBMI(BMI bmi)
        {
            dbEntities.BMIs.Add(bmi);
            return (dbEntities.SaveChanges() != 0);
        }

        internal IEnumerable<BMI> ListAllBMIs(Guid guid)
        {
            return dbEntities.BMIs.Where((b) => b.UserId == guid).OrderByDescending((b) => b.RecordTime).Take(5);
        }
    }
}