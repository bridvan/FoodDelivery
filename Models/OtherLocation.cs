using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
   public class OtherLocation
    {
        public int Id { get; set; }      
        [Required]
        public string LocationName { get; set; }
        [Required]
        public string LocationAddress { get; set; }
     
        [ForeignKey("Vendor")]
        public int VendorID { get; set; }

    }
}
