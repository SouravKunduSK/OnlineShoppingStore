using MyOnlineShoppingStore.Database;
using MyOnlineShoppingStore.Repository;
using MyOnlineShoppingStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using System.Data.SqlClient;

namespace MyOnlineShoppingStore.Controllers
{
    public class UserController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        // GET: User
        public ActionResult Register(bool Issuccess = false)
        {
            ViewBag.issuccess = Issuccess;

            return View();
        }

        [HttpPost]
        public ActionResult Register(Tbl_User tbl, Registration model)
        {
            _unitOfWork.GetRepositoryInstance<Tbl_User>().GetAllRecords();
            using (var context = new MyOnlineShoppingEntities())
            {
                bool IsNotValid = context.Tbl_User.Any(x => x.Email == model.Email || x.MobielNo == model.MobielNo);
                if (IsNotValid)
                {
                    ViewBag.DuplicateMessage = "This Email or Mobile Number has already been used! Try another...";
                    return View();
                }
                else
                {
                    tbl.CreatedOn = DateTime.Now;
                    _unitOfWork.GetRepositoryInstance<Tbl_User>().Add(tbl);
                    ViewBag.SuccessMessage = "Account Created Successfully..";
                    return View();
                }
            }
        }

        public ActionResult Login()
        {

            return View();
        }

        [HttpPost]

        public ActionResult Login(Models.LogIn model)
        {
            _unitOfWork.GetRepositoryInstance<Tbl_User>().GetAllRecords();

            using (var context = new MyOnlineShoppingEntities())
            {
                bool IsValid = context.Tbl_User.Any(x => x.UserName == model.UserName && x.Password == model.Password && x.Email == model.Email);
                if (IsValid)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                    Session["userName"] = model.UserName.ToString();
                    return RedirectToAction("HomePage", "Home");
                }

                //ViewBag.DuplicateMessage = "Incorrect Email or Password! Try again...";
                ModelState.AddModelError("", "Incorrect Email or Password! Try again...");
                return View();
            }

        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Index", "Home");
        }

        //public List<SelectListItem>GetUserId()
        //{
        //    List<SelectListItem> list = new List<SelectListItem>();
        //    var user = _unitOfWork.GetRepositoryInstance<Tbl_User>().GetAllRecords();
        //    foreach (var item in user)
        //    {
        //        list.Add(new SelectListItem { Value = item.UserId.ToString()});
        //    }
        //    return list;
        //}

        public ActionResult SellerRegister()
        {
            return View();
        }

        [HttpPost]
        public ActionResult SellerRegister(Tbl_Seller tbl, RegisterAsSeller model)
        {

            _unitOfWork.GetRepositoryInstance<Tbl_Seller>().GetAllRecords();
            using (var context = new MyOnlineShoppingEntities())
            {
                bool IsNotValid = context.Tbl_Seller.Any(x => x.UserName == model.UserName);
                if (IsNotValid)
                {
                    ViewBag.DuplicateMessage = "This Seller Name has already taken! Please, try unique one...";
                    return View();
                }
                else
                {

                    tbl.CreatedOn = DateTime.Now;
                    _unitOfWork.GetRepositoryInstance<Tbl_Seller>().Add(tbl);
                    ViewBag.SuccessMessage = "Account Created Successfully... Login Now...";

                    return View();

                }
            }
        }

        public ActionResult LoginAsSeller()
        {

            return View();
        }

        [HttpPost]

        public ActionResult LoginAsSeller(Models.LoginAsSeller model1)
        {
            _unitOfWork.GetRepositoryInstance<Tbl_Seller>().GetAllRecords();

            using (var context = new MyOnlineShoppingEntities())
            {
                bool IsValid = context.Tbl_Seller.Any(x => x.UserName == model1.UserName && x.Password == model1.Password);
                if (IsValid)
                {
                    FormsAuthentication.SetAuthCookie(model1.UserName, true);
                    Session["userName"] = model1.UserName.ToString();
                    return RedirectToAction("Dashboard", "Admin");
                }

                //ViewBag.DuplicateMessage = "Incorrect Email or Password! Try again...";
                ModelState.AddModelError("", "Incorrect Seller Name or Password! Try again...");
                return View();
            }

        }

        public ActionResult Shipping()
        {

            return View();
        }

        [HttpPost]

        public ActionResult Shipping(Tbl_ShippingDetails tbl)
        {
            _unitOfWork.GetRepositoryInstance<Tbl_ShippingDetails>().GetAllRecords();
            _unitOfWork.GetRepositoryInstance<Tbl_ShippingDetails>().Add(tbl);
            ModelState.Clear();
            ViewBag.SuccessMessage = "Your Order is placed. Product will deliver to the given Address...";

            return View();
        }
    }
}