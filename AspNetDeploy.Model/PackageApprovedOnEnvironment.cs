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
    
    public partial class PackageApprovedOnEnvironment
    {
        public int PackageId { get; set; }
        public int EnvironmentId { get; set; }
        public int ApprovedByUserId { get; set; }
        public System.DateTime ApprovedDate { get; set; }
    
        public virtual Environment Environment { get; set; }
        public virtual Package Package { get; set; }
        public virtual User ApprovedBy { get; set; }
    }
}