using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KAMS.Models
{
    public class ApartmantAidatVDetail
    {
        public IEnumerable<KAMS.Models.ApartmanAidat> Aidats { get; set; }
        public IEnumerable<KAMS.Models.ApartmantDetails> ApartmantDetails { get; set; }
        public IEnumerable<KAMS.Models.ApartmantBlocs> Blocs { get; set; }  
    }
}