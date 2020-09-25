using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models
{
    public  class Driver
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Address_Location { get; set; }
        public int AreaId { get; set; }
        public Area Area { get; set; }
        public string UserId { get; set; }
    }
}
