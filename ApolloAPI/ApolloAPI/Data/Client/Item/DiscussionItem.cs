using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApolloAPI.Data.Client.Item
{
    public abstract class DiscussionItem
    {
        public Guid DiscussionId { get; set; }
        public string Title { get; set; }
        public Person Creator { get; set; }
    }

    public class DiscussionGeneralItem : DiscussionItem
    {
        public int ReplyCount { get; set; }
        public DateTime LastActive { get; set; }
    }

    public class DiscussionDetailedItem : DiscussionItem
    {
        public IEnumerable<ReplyItem> Replies { get; set; }
    }

    public class ReplyItem
    {
        public Person Responder { get; set; }
        public string Content { get; set; }
        public DateTime RepliedAt { get; set; }
    }

    public class Person
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string ProfileImage { get; set; }
    }
}