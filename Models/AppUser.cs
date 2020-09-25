using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace Models
{
    public class AppUser : IdentityUser
    {
        //public AppUser()
        //{
        //    Vendors = new Vendor();
        //}

        [Required]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [MaxLength(50)]
        public string LastName { get; set; }
        public DateTime Created { get; set; }
        [MaxLength(200)]
        public string UniqueFileName { get; set; }
        //public virtual Vendor Vendors { get; set; }
        //public virtual Driver Driver { get; set; }
    }
}
