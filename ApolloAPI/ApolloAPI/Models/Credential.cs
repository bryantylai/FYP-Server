//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ApolloAPI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Credential
    {
        public System.Guid Id { get; set; }
        public System.Guid PersonId { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Role Role { get; set; }
        public System.DateTime CreatedAt { get; set; }
        public System.DateTime LastLogin { get; set; }
    }
}
