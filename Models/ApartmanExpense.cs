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
    
    public partial class ApartmanExpense
    {
        public int ID { get; set; }
        public Nullable<int> AID { get; set; }
        public Nullable<int> EpenseType { get; set; }
        public string AÇIKLAMA { get; set; }
        public Nullable<System.DateTime> TARIH { get; set; }
        public Nullable<decimal> TUTAR { get; set; }
    
        public virtual ApartmantDetails ApartmantDetails { get; set; }
        public virtual EXPENSETYPE EXPENSETYPE { get; set; }
    }
}