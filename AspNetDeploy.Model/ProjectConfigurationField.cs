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
    
    public partial class ProjectConfigurationField
    {
        public ProjectConfigurationField()
        {
            this.ProjectConfigurationValue = new HashSet<ProjectConfigurationValue>();
            this.Project = new HashSet<Project>();
        }
    
        public int Id { get; set; }
        public int ProjectId { get; set; }
        public int TypeId { get; set; }
        public string Key { get; set; }
        public bool IsSensitive { get; set; }
        public int ModeId { get; set; }
    
        public virtual ICollection<ProjectConfigurationValue> ProjectConfigurationValue { get; set; }
        public virtual ICollection<Project> Project { get; set; }
    }
}