using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVC.Models
{
    public class mvcModelCustom
    {
        public IEnumerable<mvcCategoryModel> mvcCategory { get; set; }

        public IEnumerable<mvcNewModel> mvcNew { get; set; }

        public mvcNewModel newModel { get; set; }
    }
}