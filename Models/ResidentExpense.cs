//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace KAMS.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class ResidentExpense
    {
        public int ID { get; set; }
        public Nullable<int> RAID { get; set; }
        public Nullable<int> REVENETYPE { get; set; }
        public Nullable<System.DateTime> DATE { get; set; }
        public Nullable<decimal> TUTAR { get; set; }
        public Nullable<int> REID { get; set; }
    
        public virtual ResidentAccount ResidentAccount { get; set; }
        public virtual REVUNUETYPE REVUNUETYPE { get; set; }
        public virtual RESIDENT RESIDENT { get; set; }
    }
}
