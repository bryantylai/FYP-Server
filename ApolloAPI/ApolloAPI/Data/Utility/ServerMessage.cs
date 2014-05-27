using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ApolloAPI.Data.Client.Item;

namespace ApolloAPI.Data.Utility
{
    public class ServerMessage
    {
        public bool IsError { get; set; }
        public string Message { get; set; }
    }

    public class LoginMessage : ServerMessage
    {
        public bool NewAccount { get; set; }
    }

    public class BMIMessage : ServerMessage
    {
        public double Height { get; set; }
        public double Weight { get; set; }
    }

    public class RunMessage : ServerMessage
    {
        public long Duration { get; set; }
        public AvatarProfileItem Avatar { get; set; }
    }
}