//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace AspNetDeploy.Model
{
    using System;
    using System.Collections.Generic;
    
    public partial class AspNetDeployExceptionLog
    {
        public int Id { get; set; }
        public int ExceptionId { get; set; }
        public System.DateTime TimeStamp { get; set; }
        public Nullable<int> UserId { get; set; }
    
        public virtual ExceptionLog Exception { get; set; }
        public virtual User User { get; set; }
    }
}