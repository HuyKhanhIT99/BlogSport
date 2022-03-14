using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC.Models
{
    public class mvcNewModel
    {
        public long ID { get; set; }
        public long CATEGORY_ID { get; set; }
        public string NAME { get; set; }
        public string DESCRIPTION { get; set; }
        public string DETAIL { get; set; }
        public string IMAGE { get; set; }
        public Nullable<System.DateTime> DATE { get; set; }
        public long USER_ID { get; set; }
        public IEnumerable<SelectListItem> CategoryList { get; set; }
        public IEnumerable<SelectListItem> UserList { get; set; }
    }
}