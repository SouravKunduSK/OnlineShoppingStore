using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyOnlineShoppingStore.Models
{
    public class ShippingDetail
    {
        public int ShippingId { get; set; }
        public Nullable<int> UserId { get; set; }
        [Required]
        [StringLength(1000, ErrorMessage = "Address Required.", MinimumLength = 3)]
        [Display(Name = "Give Address")]
        public string Address { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "District Name Required.", MinimumLength = 3)]
        [Display(Name = "District Name")]
        public string District { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Zip Code Required.", MinimumLength = 3)]
        [Display(Name = "Zip Code")]
        public Nullable<int> ZipCode { get; set; }
        public Nullable<int> OrderId { get; set; }
        public Nullable<decimal> ShippingCharge { get; set; }
        public Nullable<decimal> Amount { get; set; }
    }
}