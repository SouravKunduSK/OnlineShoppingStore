using OnlineShoppingStore.DAL;
using OnlineShoppingStore.Repository;
using PagedList;
using PagedList.Mvc;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace OnlineShoppingStore.Models.Home
{
    public class HomeIndexViewModel
    {
        public GenericUnitOfWork _unitOfWork = new GenericUnitOfWork();
        dbMyOnlineShoppingStoreEntities context = new dbMyOnlineShoppingStoreEntities();
        public IPagedList<tbl_Product> ListOfProducts { get; set; }
        public HomeIndexViewModel CreateModel(string search, int pageSize, int? page)
        {
            SqlParameter[] param = new SqlParameter[]
            {
                new SqlParameter("@search",search??(object)DBNull.Value)
            };
            IPagedList<tbl_Product> data = context.Database.SqlQuery<tbl_Product>("GetBySearch @search", param).ToList().ToPagedList(page ?? 1,  pageSize);
            return new HomeIndexViewModel
            {
                ListOfProducts = data
            };
        }
    }
}