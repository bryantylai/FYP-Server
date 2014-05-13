using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ApolloAPI.Data.Client.Item
{
    public class LeaderboardItem
    {
        public Guid LeaderboardId { get; set; }
        public Guid PlayerId { get; set; }
        public string PlayerProfileImage { get; set; }
        public string PlayerName { get; set; }
        public string Point { get; set; }
        public bool IsSelf { get; set; }
    }
}