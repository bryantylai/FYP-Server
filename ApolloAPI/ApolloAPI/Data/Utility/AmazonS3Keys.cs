using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApolloAPI.Data.Utility
{
    public class AmazonS3Keys
    {
        public string BucketName { get; set; }
        public string AccessKey { get; set; }
        public string SecretKey { get; set; }

        public AmazonS3Keys()
        {
            BucketName = "apollo-ws";
            AccessKey = "AKIAJBA4UZU67VMP3V3A";
            SecretKey = "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";
        }
    }
}