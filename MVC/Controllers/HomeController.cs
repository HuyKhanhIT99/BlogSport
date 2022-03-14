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
        private mvcCategoryModel mvcCategoryModel = new mvcCategoryModel();

        mvcModelCustom modelCustom = new mvcModelCustom();

        // GET: Home
        public ActionResult Index()
        {
            GetListCategories();
            return View(new mvcModelCustom() { mvcCategory = mvcCategoryModel.categorylist });
        }
        public ActionResult FindNewByCategoriesID(long? id)
        {
            GetListCategories();
            IEnumerable<mvcNewModel> newsList;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("news1/" + id.ToString()).Result;
            newsList = response.Content.ReadAsAsync<IEnumerable<mvcNewModel>>().Result;
            return View(new mvcModelCustom(){ mvcCategory = mvcCategoryModel.categorylist , mvcNew = newsList});
        }
        public void GetListCategories()
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("categories").Result;
            mvcCategoryModel.categorylist = response.Content.ReadAsAsync<IEnumerable<mvcCategoryModel>>().Result;
        }
        public ActionResult Detail(long? id)
        {
            GetListCategories();
            mvcNewModel ns = new mvcNewModel();
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("news/" + id.ToString()).Result;
            ns = response.Content.ReadAsAsync<mvcNewModel>().Result;
            
            return View(new mvcModelCustom() { mvcCategory = mvcCategoryModel.categorylist, newModel =ns });
        }
    }
}