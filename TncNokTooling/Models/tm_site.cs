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
    
    public partial class tm_site
    {
        public tm_site()
        {
            this.td_pr = new HashSet<td_pr>();
            this.tm_user = new HashSet<tm_user>();
        }
    
        public byte site_id { get; set; }
        public string site_name { get; set; }
    
        public virtual ICollection<td_pr> td_pr { get; set; }
        public virtual ICollection<tm_user> tm_user { get; set; }
    }
}