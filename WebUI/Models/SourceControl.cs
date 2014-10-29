//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebUI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class SourceControl
    {
        public SourceControl()
        {
            this.Project = new HashSet<Project>();
            this.Properties = new HashSet<SourceControlProperty>();
            this.Group = new HashSet<Group>();
        }
    
        public int Id { get; set; }
        public string Name { get; set; }
        public int TypeId { get; set; }
        public bool IsDeleted { get; set; }
    
        public virtual ICollection<Project> Project { get; set; }
        public virtual ICollection<SourceControlProperty> Properties { get; set; }
        public virtual ICollection<Group> Group { get; set; }
    }
}