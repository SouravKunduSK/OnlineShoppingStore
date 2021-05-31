using MyOnlineShoppingStore.Database;
using MyOnlineShoppingStore.Models;
using MyOnlineShoppingStore.Repository;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MyOnlineShoppingStore.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public ActionResult Dashboard()
        {
            return View();
        }

          public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();

        public ActionResult Categories()
        {

            return View(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetProduct());

        }

        public ActionResult AddCategory()
        {
            return UpdateCategory(0);
        }


        [HttpPost]
        public ActionResult UpdateCategory(Tbl_Category tbl)
        {

            _unitOfWork.GetRepositoryInstance<Tbl_Category>().Add(tbl);
            return RedirectToAction("Categories");
        }

        public ActionResult UpdateCategory(int categoryId)
        {
            Category cd;
            if (categoryId != 0)
            {
                cd = JsonConvert.DeserializeObject<Category>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(categoryId)));
            }
            else
            {

                cd = new Category();
            }
            return View("UpdateCategory", cd);

        }
        public ActionResult CategoryEdit(int catId)
        {

            return View(_unitOfWork.GetRepositoryInstance<Tbl_Category>().GetFirstorDefault(catId));
        }

        [HttpPost]
        public ActionResult CategoryEdit(Tbl_Category tbl)
        {

            _unitOfWork.GetRepositoryInstance<Tbl_Category>().Update(tbl);
            return RedirectToAction("Categories");
        }


        public List<SelectListItem> GetCategory()

        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<Tbl_Category>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            return list;
        }

        public List<SelectListItem> GetLocation()

        {
            List<SelectListItem> list1 = new List<SelectListItem>();
            var loc = _unitOfWork.GetRepositoryInstance<Tbl_Location>().GetAllRecords();
            foreach (var item in loc)
            {
                list1.Add(new SelectListItem { Value = item.LocationId.ToString(), Text = item.LocationName });
            }
            return list1;
        }
      
        public ActionResult Locations()
        {

            return View(_unitOfWork.GetRepositoryInstance<Tbl_Location>().GetProduct());

        }

        public ActionResult AddLocation()
        {
            return UpdateLocation(0);
        }


        [HttpPost]
        public ActionResult UpdateLocation(Tbl_Location tbl)
        {

            _unitOfWork.GetRepositoryInstance<Tbl_Location>().Add(tbl);
            return RedirectToAction("Locations");
        }

        public ActionResult UpdateLocation(int locId)
        {
            Location cd;
            if (locId != 0)
            {
                cd = JsonConvert.DeserializeObject<Location>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<Tbl_Location>().GetFirstorDefault(locId)));
            }
            else
            {

                cd = new Location();
            }
            return View("UpdateLocation", cd);

        }

        public ActionResult LocationEdit(int locId)
        {

            return View(_unitOfWork.GetRepositoryInstance<Tbl_Location>().GetFirstorDefault(locId));
        }

        [HttpPost]
        public ActionResult LocationEdit(Tbl_Location tbl)
        {

            _unitOfWork.GetRepositoryInstance<Tbl_Location>().Update(tbl);
            return RedirectToAction("Locations");
        }

        
        public ActionResult Product()
        {
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetProduct());
        }

        public ActionResult ProductEdit(int productId)
        {
            ViewBag.CategoryList = GetCategory();
            ViewBag.CategoryList1 = GetLocation();
            return View(_unitOfWork.GetRepositoryInstance<Tbl_Product>().GetFirstorDefault(productId));
        }

        [HttpPost]
        public ActionResult ProductEdit(Tbl_Product tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImages/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.ProductImage = file != null ? pic : tbl.ProductImage;

            tbl.ModifiedOn = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Product>().Update(tbl);
            return RedirectToAction("Product");
        }

        public ActionResult ProductAdd()
        {
            ViewBag.CategoryList = GetCategory();
            ViewBag.CategoryList1 = GetLocation();
            return View();
        }
        [HttpPost]
        public ActionResult ProductAdd(Tbl_Product tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImages/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.ProductImage = pic;

            tbl.CreatedOn = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<Tbl_Product>().Add(tbl);

            return RedirectToAction("Product");
        }
    }
    
}