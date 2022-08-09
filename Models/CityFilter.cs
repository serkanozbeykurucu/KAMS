using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KAMS.Models
{
    public class CityFilter
    {
        public IEnumerable<SelectListItem> COUNTRY { get; set; }
        public IEnumerable<SelectListItem> CITY { get; set; }
        public IEnumerable<SelectListItem> TOWNS { get; set; }
        public IEnumerable<SelectListItem> DISTRICT { get; set; }
        public IEnumerable<KAMS.Models.USERS> USERS { get; set; }
    }
}