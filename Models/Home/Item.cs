using MyOnlineShoppingStore.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MyOnlineShoppingStore.Models.Home
{
    public class Item
    {
        public Tbl_Product Product { get; set; }
        public int Quantity { get; set; }

        internal object FirstOrDefault()
        {
            throw new NotImplementedException();
        }
    }
}