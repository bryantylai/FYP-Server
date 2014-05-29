﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApolloAPI.Data
{
    public abstract class DiscussionItem
    {
        public Guid DiscussionId { get; set; }
        public string Title { get; set; }
        public string CreatorName { get; set; }
    }

    public class DiscussionGeneralItem : DiscussionItem
    {
        public int ReplyCount { get; set; }
        public long LastActive { get; set; }
    }

    public class DiscussionDetailedItem : DiscussionItem
    {
        public IEnumerable<ReplyItem> Replies { get; set; }
    }

    public class ReplyItem
    {
        public string ResponderName { get; set; }
        public string Content { get; set; }
        public long RepliedAt { get; set; }
    }
}