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
    
    public partial class tm_product
    {
        public tm_product()
        {
            this.td_pr = new HashSet<td_pr>();
            this.tm_leadtime = new HashSet<tm_leadtime>();
        }
    
        public byte id { get; set; }
        public string name { get; set; }
    
        public virtual ICollection<td_pr> td_pr { get; set; }
        public virtual ICollection<tm_leadtime> tm_leadtime { get; set; }
    }
}
