using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApolloAPI.Data.Doctors.Item
{
    public abstract class DiscussionItem
    {
        public Guid DiscussionId { get; set; }
        public string Title { get; set; }
    }

    public class DiscussionDetailedItem : DiscussionItem
    {
        public int ReplyCount { get; set; }
        public DateTime LastActive { get; set; }
    }

    public class DiscussionGeneralItem : DiscussionItem
    {

    }
}