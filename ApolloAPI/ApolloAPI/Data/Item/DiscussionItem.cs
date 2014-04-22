using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApolloAPI.Data.Item
{
    public class DiscussionItem
    {
        public Guid DiscussionId { get; set; }
        public string Title { get; set; }
        public int ReplyCount { get; set; }
        public DateTime LastActive { get; set; }
    }
}