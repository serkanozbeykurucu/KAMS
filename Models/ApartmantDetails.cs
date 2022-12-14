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
    using System.Web.Mvc;

    public partial class ApartmantDetails
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public ApartmantDetails()
        {
            this.AMU = new HashSet<AMU>();
            this.ApartmanAidat = new HashSet<ApartmanAidat>();
            this.ApartmanCasa = new HashSet<ApartmanCasa>();
            this.ApartmanExpense = new HashSet<ApartmanExpense>();
            this.ApartmanRevenue = new HashSet<ApartmanRevenue>();
            this.ApartmantBlocs = new HashSet<ApartmantBlocs>();
            this.RESIDENT = new HashSet<RESIDENT>();
            this.KATLAR = new HashSet<KATLAR>();
            this.DAIRELER = new HashSet<DAIRELER>();
        }
    
        public int ID { get; set; }
        public string NAME { get; set; }
        public int CID { get; set; }
        public int CTID { get; set; }
        public int TWID { get; set; }
        public int DTID { get; set; }
        public string C_ADDRESS { get; set; }

        public IEnumerable<SelectListItem> ct { get; set; }
        public IEnumerable<SelectListItem> c { get; set; }
        public IEnumerable<SelectListItem> t { get; set; }
        public IEnumerable<SelectListItem> d { get; set; }


        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<AMU> AMU { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApartmanAidat> ApartmanAidat { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApartmanCasa> ApartmanCasa { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApartmanExpense> ApartmanExpense { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApartmanRevenue> ApartmanRevenue { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<ApartmantBlocs> ApartmantBlocs { get; set; }
        public virtual CITY CITY { get; set; }
        public virtual COUNTRY COUNTRY { get; set; }
        public virtual DISTRICTS DISTRICTS { get; set; }
        public virtual TOWNS TOWNS { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<RESIDENT> RESIDENT { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<KATLAR> KATLAR { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<DAIRELER> DAIRELER { get; set; }
    }
}
