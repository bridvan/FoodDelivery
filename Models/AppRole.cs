
using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class AppRole : IdentityRole
    {
        [Required]
        [MaxLength(400)]
        public string Discription { get; set; }
        [Required]
        public DateTime Created { get; set; }
    }   
}
