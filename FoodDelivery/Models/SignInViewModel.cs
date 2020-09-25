using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Models
{
    public class SignInViewModel
    {
        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} must be from {2} - {1} characters", MinimumLength = 8)]
        [EmailAddress(ErrorMessage = "{0} is invalid")]
        public string UserName { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} must be from {2} - {1} characters", MinimumLength = 8)]
        public string Password { get; set; }

        [Required]
        public bool RememberMe { get; set; }
    }
}
