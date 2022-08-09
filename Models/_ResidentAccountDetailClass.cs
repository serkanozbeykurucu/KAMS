using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KAMS.Models
{
    public class _ResidentAccountDetailClass
    {
        public IEnumerable<KAMS.Models.ResidentAccount> residentAccounts { get; set; }
        public IEnumerable<KAMS.Models.RESIDENT> resident { get; set; }
        public IEnumerable<KAMS.Models.COUNTRY> country { get; set; }
        public IEnumerable<KAMS.Models.CITY> city { get; set; }
        public IEnumerable<KAMS.Models.TOWNS> towns { get; set; }
        public IEnumerable<KAMS.Models.DISTRICTS> districts { get; set; }
        public IEnumerable<KAMS.Models.ApartmantDetails> apartmantDetails { get; set; }
        public IEnumerable<KAMS.Models.ApartmantBlocs> apartmantBlocs { get; set; }
        public IEnumerable<KAMS.Models.KATLAR> katlar { get; set; }
        public IEnumerable<KAMS.Models.DAIRELER> daireler { get; set; }


    }
}