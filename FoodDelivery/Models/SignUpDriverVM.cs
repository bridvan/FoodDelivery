using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Models
{
    public class SignUpDriverVM
    {
        public SignUpDriverVM()
        {
            Area = new List<SelectListItem>();
        }
        public string Id { get; set; }

        [Display(Name = "First Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} must be from {2} - {1} characters", MinimumLength = 3)]
        public string FirstName { get; set; }

        [Display(Name = "Last Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} must be from {2} - {1} characters", MinimumLength = 3)]
        public string LastName { get; set; }

        [Display(Name = "Phone Number")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(20, ErrorMessage = "{0} must be from {2} - {1} characters", MinimumLength = 3)]
        public string PhoneNumber { get; set; }

        [Display(Name = "Email")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(150, ErrorMessage = "{0} must be max. of {1} characters")]
        [EmailAddress(ErrorMessage = "{0} is invalid")]
        public string Email { get; set; }

        [Display(Name = "Password")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} must be from {2} - {1} characters", MinimumLength = 8)]
        public string Password { get; set; }


        [Display(Name = "Address")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(150, ErrorMessage = "{0} must be max. of {1} characters")]
        public string Address { get; set; }

        [Display(Name = "Confirm Password")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} must be from {2} - {1} characters", MinimumLength = 8)]
        [Compare("Password", ErrorMessage = "Password does not match")]
        public string ConfirmPassword { get; set; }

        [Display(Name = "Area")]
        [Required(ErrorMessage = "{0} is required")]
        public int AreaId { get; set; }
        public List<SelectListItem> Area { get; set; }



    }
}
