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
    
    public partial class Comment
    {
        public System.Guid Id { get; set; }
        public System.Guid PostId { get; set; }
        public string Content { get; set; }
        public System.Guid CommentedBy { get; set; }
        public System.DateTime CommentedAt { get; set; }
    }
}
