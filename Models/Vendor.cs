using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public class Vendor
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public string Website_Url { get; set; }
        public string NumberOfLocation { get; set; }
        [Required]
        [MaxLength(100)]
        public string Address_Location { get; set; }
        public int AreaId { get; set; }
        public Area Area { get; set; }
        public int CuisineId { get; set; }
        public Cuisine Cuisine { get; set; }
        public string UserId { get; set; }
        public string UniqueFileName { get; set; }
    }
}
