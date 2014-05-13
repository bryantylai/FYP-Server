using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApolloAPI.Data.Client.Form
{
    public class ReplyForm
    {
        public Guid DiscussionId { get; set; }
        public string Content { get; set; }
    }
}