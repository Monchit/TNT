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
    
    public partial class TM_ResetCountPoint
    {
        public TM_ResetCountPoint()
        {
            this.TM_Program = new HashSet<TM_Program>();
        }
    
        public byte Id { get; set; }
        public string Point { get; set; }
        public string PointPattern { get; set; }
    
        public virtual ICollection<TM_Program> TM_Program { get; set; }
    }
}
