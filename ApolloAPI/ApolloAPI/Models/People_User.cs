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
    
    public partial class People_User
    {
        public People_User()
        {
            this.BMIs = new HashSet<BMI>();
        }
    
        public System.Guid id { get; set; }
    
        public virtual ICollection<BMI> BMIs { get; set; }
        public virtual Person Person { get; set; }
    }
}
