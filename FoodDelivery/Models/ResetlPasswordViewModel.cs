using System;
using System.ComponentModel.DataAnnotations;
namespace FoodDelivery.Models
{
    public class ResetlPasswordViewModel
    {
        [Display(Name = "User Name")]
        [Required(ErrorMessage = "{0} is required")]
        [StringLength(50, ErrorMessage = "{0} must be from {2} - {1} characters", MinimumLength = 8)]
        public string UserName { get; set; }
    }
}
