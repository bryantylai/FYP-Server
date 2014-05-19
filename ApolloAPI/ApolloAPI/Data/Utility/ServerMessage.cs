using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

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
}