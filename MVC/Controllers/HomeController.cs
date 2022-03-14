using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace MVC.Controllers
{
    public class HomeController : Controller
    {
        // GET: Home
        public ActionResult Index()
        {
            IEnumerable<mvcCategoryModel> categorylist;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("categories").Result;
            categorylist = response.Content.ReadAsAsync<IEnumerable<mvcCategoryModel>>().Result;
            return View(categorylist);
        }
    }
}