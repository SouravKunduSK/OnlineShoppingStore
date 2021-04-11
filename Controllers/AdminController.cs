﻿using Newtonsoft.Json;
using OnlineShoppingStore.DAL;
using OnlineShoppingStore.Models;
using OnlineShoppingStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Controllers
{
    public class AdminController : Controller
    {
        // GET: Admin
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        public List<SelectListItem> GetCategory()
        
        {
            List<SelectListItem> list = new List<SelectListItem>();
            var cat = _unitOfWork.GetRepositoryInstance<tbl_Category>().GetAllRecords();
            foreach (var item in cat)
            {
                list.Add(new SelectListItem { Value = item.CategoryId.ToString(), Text = item.CategoryName });
            }
            return list;
        }

        public List<SelectListItem> GetLocation()

        {
            List<SelectListItem> list1 = new List<SelectListItem>();
            var loc = _unitOfWork.GetRepositoryInstance<tbl_Location>().GetAllRecords();
            foreach (var item in loc)
            {
                list1.Add(new SelectListItem { Value = item.LocationId.ToString(), Text = item.LocationName });
            }
            return list1;
        }
        public ActionResult Dashboard()
        {
            return View();
        }

        public ActionResult Categories()
        {
            List<tbl_Category> allcategories = _unitOfWork.GetRepositoryInstance<tbl_Category>().GetAllRecordsIQueryable().Where(i => i.IsDelete == false).ToList();
            return View(allcategories);
        }

        public ActionResult AddCategory()
        {
            return UpdateCategory(0);
        }

      

        public ActionResult UpdateCategory(int categoryId)
        {
            CategoryDetail cd;
            if (categoryId != 0)
            {
                cd = JsonConvert.DeserializeObject<CategoryDetail>(JsonConvert.SerializeObject(_unitOfWork.GetRepositoryInstance<tbl_Category>().GetFirstorDefault(categoryId)));
            }
            else
            {
                cd = new CategoryDetail();
            }
            return View("UpdateCategory", cd);

        }

        public ActionResult CategoryEdit(int catId)
        {
            
            return View(_unitOfWork.GetRepositoryInstance<tbl_Category>().GetFirstorDefault(catId));
        }

        [HttpPost]
        public ActionResult CategoryEdit(tbl_Category tbl)
        {
            
            _unitOfWork.GetRepositoryInstance<tbl_Category>().Update(tbl);
            return RedirectToAction("Categories");
        }

        public ActionResult Product()
        {
            return View(_unitOfWork.GetRepositoryInstance<tbl_Product>().GetProduct());
        }

        public ActionResult ProductEdit(int productId)
        {
            ViewBag.CategoryList = GetCategory();
            ViewBag.CategoryList1 = GetLocation();
            return View(_unitOfWork.GetRepositoryInstance<tbl_Product>().GetFirstorDefault(productId));
        }

        [HttpPost]
        public ActionResult ProductEdit(tbl_Product tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.ProductImage = file != null ? pic : tbl.ProductImage;

            tbl.ModifiedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<tbl_Product>().Update(tbl);
            return RedirectToAction("Product");
        }

        public ActionResult ProductAdd()
        {
            ViewBag.CategoryList = GetCategory();
            ViewBag.CategoryList1 = GetLocation();
            return View();
        }
        [HttpPost]
        public ActionResult ProductAdd(tbl_Product tbl, HttpPostedFileBase file)
        {
            string pic = null;
            if (file != null)
            {
                pic = System.IO.Path.GetFileName(file.FileName);
                string path = System.IO.Path.Combine(Server.MapPath("~/ProductImg/"), pic);
                // file is uploaded
                file.SaveAs(path);
            }
            tbl.ProductImage = pic;

            tbl.CreatedDate = DateTime.Now;
            _unitOfWork.GetRepositoryInstance<tbl_Product>().Add(tbl);
            return RedirectToAction("Product");
        }
    }



}