using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApolloAPI.Data
{
    public class BMIResult
    {
        public BMIResult(Guid id, double height, double weight, DateTime recordTime)
        {
            this.Id = id;
            this.Result = weight / Math.Pow(height, 2);
            this.RecordTime = recordTime;
        }

        public Guid Id { get; set; }
        public double Result { get; set; }
        public DateTime RecordTime { get; set; }
    }
}