using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MyOnlineShoppingStore.Models
{
    public class RegisterAsSeller
    {
        public int SellerId { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "Seller Name Required.", MinimumLength = 3)]
        [Display(Name = "Set Seller Name")]
        public string UserName { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Set Seller Password")]
        public string Password { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Confirm Seller Password")]
        [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
        public string ConfirmPassword { get; set; }
        public Nullable<int> UserId { get; set; }
        public Nullable<System.DateTime> CreatedOn { get; set; }
    }

    public class LoginAsSeller
    {
        [Required]
        [StringLength(100, ErrorMessage = "Seller Name Required.", MinimumLength = 3)]
        [Display(Name = "Seller Name")]
        public string UserName { get; set; }
        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 6)]
        [DataType(DataType.Password)]
        [Display(Name = "Seller Password")]
        public string Password { get; set; }
    }
}