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
    
    public partial class ResidentAccount
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ResidentAccount()
        {
            this.ResidentExpense = new HashSet<ResidentExpense>();
            this.ResidentRevenus = new HashSet<ResidentRevenus>();
        }
    
        public int ID { get; set; }
        public Nullable<int> RESIDENTID { get; set; }
        public Nullable<decimal> BAKİYE { get; set; }
        public Nullable<decimal> RTOTAL { get; set; }
        public Nullable<decimal> ETOTAL { get; set; }
    
        public virtual RESIDENT RESIDENT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResidentExpense> ResidentExpense { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ResidentRevenus> ResidentRevenus { get; set; }
    }
}
