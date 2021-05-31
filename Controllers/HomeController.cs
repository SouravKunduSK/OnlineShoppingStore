using MyOnlineShoppingStore.Database;
using MyOnlineShoppingStore.Models;
using MyOnlineShoppingStore.Models.Home;
using MyOnlineShoppingStore.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using PagedList.Mvc;
using System.Data.SqlClient;
using Newtonsoft.Json;
using System.Web.Security;
using System.Configuration;


namespace MyOnlineShoppingStore.Controllers
{
    
    public class HomeController : Controller
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        MyOnlineShoppingEntities ctx = new MyOnlineShoppingEntities();
        public ActionResult Index(string search, int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search, 8, page));
        }

        public ActionResult HomePage(string search, int? page)
        {
            HomeIndexViewModel model = new HomeIndexViewModel();
            return View(model.CreateModel(search, 8, page));
        }

        public ActionResult AddToCart(int productId)
        {
            if (Session["login"] == null)
            {
                List<Item> cart = new List<Item>();
                var product = ctx.Tbl_Product.Find(productId);
                cart.Add(new Item()
                {
                    Product = product,
                    Quantity = 1
                });
                Session["login"] = cart;
            }
            else
            {
                List<Item> cart = (List<Item>)Session["login"];
                var count = cart.Count();
                var product = ctx.Tbl_Product.Find(productId);
                for (int i = 0; i < count; i++)
                {
                    if (cart[i].Product.ProductId == productId)
                    {
                        int prevQty = cart[i].Quantity;
                        cart.Remove(cart[i]);
                        cart.Add(new Item()
                        {
                            Product = product,
                            Quantity = prevQty + 1
                        });
                        break;
                    }
                    else
                    {
                        var prd = cart.Where(x => x.Product.ProductId == productId).SingleOrDefault();
                        if (prd == null)
                        {
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = 1
                            });
                        }
                    }
                }
                Session["login"] = cart;
            }
            return Redirect("HomePage");
        }

        public ActionResult RemoveFromCart(int productId)
        {
            List<Item> cart = (List<Item>)Session["login"];
            foreach (var item in cart)
            {
                if (item.Product.ProductId == productId)
                {
                    cart.Remove(item);
                    break;
                }
            }
            Session["login"] = cart;
            return Redirect("HomePage");
        }

        public ActionResult Cart(Tbl_Cart tbl)
        {
            _unitOfWork.GetRepositoryInstance<Tbl_Cart>().Add(tbl);
            return View();
        }


        public ActionResult CheckoutDetails()
        {
            return View();
        }

        public ActionResult DecreaseQty(int productId)
        {
            if (Session["login"] != null)
            {
                List<Item> cart = (List<Item>)Session["login"];
                var product = ctx.Tbl_Product.Find(productId);
                foreach (var item in cart)
                {
                    if (item.Product.ProductId == productId)
                    {
                        int prevQty = item.Quantity;
                        if (prevQty > 0)
                        {
                            cart.Remove(item);
                            cart.Add(new Item()
                            {
                                Product = product,
                                Quantity = prevQty - 1
                            });
                        }
                        break;
                    }
                }
                Session["login"] = cart;
            }
            return Redirect("Cart");
        }

    

    public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}