//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace TncNokTooling.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class td_tooling
    {
        public td_tooling()
        {
            this.td_eta_tnc = new HashSet<td_eta_tnc>();
        }
    
        public string pr_no { get; set; }
        public short pr_line { get; set; }
        public string description { get; set; }
        public int qty { get; set; }
        public string unit { get; set; }
        public bool sell { get; set; }
        public string kouza { get; set; }
        public string status { get; set; }
    
        public virtual td_pr td_pr { get; set; }
        public virtual ICollection<td_eta_tnc> td_eta_tnc { get; set; }
    }
}
