using MVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;


namespace MVC.Controllers
{
    public class AdminController : Controller
    {

        // GET: Admin
        public ActionResult Index()
        {

            IEnumerable<mvcCategoryModel> categorylist;
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("categories").Result;
           categorylist = response.Content.ReadAsAsync<IEnumerable<mvcCategoryModel>>().Result;
            return View(categorylist);
        }
        public ActionResult AddOrEditCategory(int id = 0)
        {
            if (id == 0)
                return View(new mvcCategoryModel());
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("categories/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcCategoryModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddOrEditCategory(mvcCategoryModel category)
        {
            if (category.ID == 0)
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("categories", category).Result;
                TempData["SuccessMessage"] = "Save Successfully";

            }
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("categories/" + category.ID, category).Result;
                TempData["SuccessMessage"] = "Update Successfully";

            }
            return RedirectToAction("Index");
        }
        public ActionResult DeleteCategory(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("categories/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Delete Successfully";
            return RedirectToAction("Index");
        }

        // danh sach bai viet
        public ActionResult News()
        {  
            HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("news").Result;
            IEnumerable<mvcNewModel> news = response.Content.ReadAsAsync<IEnumerable<mvcNewModel>>().Result;
            return View(news);
        }
        public ActionResult AddOrEditNew(int id = 0)
        {
            if (id == 0)
            {
                mvcNewModel newModel = new mvcNewModel();
                 
                IEnumerable<mvcCategoryModel> categorylist;
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("categories").Result;
                categorylist = response.Content.ReadAsAsync<IEnumerable<mvcCategoryModel>>().Result;
                HttpResponseMessage responsUsers = GlobalVariables.WebApiClient.GetAsync("users").Result;
                IEnumerable<mvcUserModel> usersList = responsUsers.Content.ReadAsAsync<IEnumerable<mvcUserModel>>().Result;
                newModel.CategoryList  = new SelectList(categorylist.ToList(), "ID", "NAME", null );
                newModel.UserList = new SelectList(usersList.ToList(), "ID", "USERNAME", null);
                return View(newModel);
            }
               
            else
            {
                HttpResponseMessage response = GlobalVariables.WebApiClient.GetAsync("news/" + id.ToString()).Result;
                return View(response.Content.ReadAsAsync<mvcNewModel>().Result);
            }
        }
        [HttpPost]
        public ActionResult AddOrEditNew(mvcNewModel news , HttpPostedFileBase image)
        {
            if (news.ID == 0)
            {
                news.IMAGE = SaveImage(image);
                HttpResponseMessage response = GlobalVariables.WebApiClient.PostAsJsonAsync("news", news).Result;
                TempData["SuccessMessage"] = "Save Successfully";

            }
            else
            {
                news.IMAGE = SaveImage(image);
                HttpResponseMessage response = GlobalVariables.WebApiClient.PutAsJsonAsync("news/" + news.ID, news).Result;
                TempData["SuccessMessage"] = "Update Successfully";

            }
            return RedirectToAction("News");
        }
        public ActionResult DeleteNew(int id)
        {
            HttpResponseMessage response = GlobalVariables.WebApiClient.DeleteAsync("news/" + id.ToString()).Result;
            TempData["SuccessMessage"] = "Delete Successfully";
            return RedirectToAction("News");
        }
        public string SaveImage(HttpPostedFileBase Image)
        {
            string FileName = "";
            if (Image != null && Image.ContentLength > 0)
            {
                FileName = "news" + Image.FileName;
                string _path = System.IO.Path.Combine(Server.MapPath("~/Upload/news"), FileName);
                Image.SaveAs(_path);
            }
            return FileName;
        }
       
    }
}