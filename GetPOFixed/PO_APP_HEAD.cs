//------------------------------------------------------------------------------
// <auto-generated>
//    This code was generated from a template.
//
//    Manual changes to this file may cause unexpected behavior in your application.
//    Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace GetPOFixed
{
    using System;
    using System.Collections.Generic;
    
    public partial class PO_APP_HEAD
    {
        public PO_APP_HEAD()
        {
            this.PO_APP_LINE = new HashSet<PO_APP_LINE>();
            this.PO_APP_TRANSACTION = new HashSet<PO_APP_TRANSACTION>();
        }
    
        public string RecordStatus { get; set; }
        public string CompanyCode { get; set; }
        public string PONO { get; set; }
        public int AppCount { get; set; }
        public Nullable<System.DateTime> EntryDate { get; set; }
        public Nullable<System.TimeSpan> EntryTime { get; set; }
        public Nullable<System.DateTime> UpdateDate { get; set; }
        public Nullable<System.TimeSpan> UpdateTime { get; set; }
        public string AppReqSign { get; set; }
        public string AppReqReceipt { get; set; }
        public string AppRepSign { get; set; }
        public string AppRepReceipt { get; set; }
        public string AppSign { get; set; }
        public Nullable<System.DateTime> AppDate { get; set; }
        public Nullable<System.TimeSpan> AppTime { get; set; }
        public string AppUser { get; set; }
        public string AppMemo { get; set; }
        public string Vendor { get; set; }
        public string Currency { get; set; }
        public Nullable<System.DateTime> POFixedDate { get; set; }
        public Nullable<System.TimeSpan> POFixedTime { get; set; }
        public string Buyer { get; set; }
        public string BuyerMemo { get; set; }
        public string Responser { get; set; }
    
        public virtual ICollection<PO_APP_LINE> PO_APP_LINE { get; set; }
        public virtual ICollection<PO_APP_TRANSACTION> PO_APP_TRANSACTION { get; set; }
    }
}
