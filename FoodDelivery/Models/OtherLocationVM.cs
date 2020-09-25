using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FoodDelivery.Models
{
    public class OtherLocationVM
    {
       
        [Required]
        public string LocationName { get; set; }
        [Required]
        public string LocationAddress { get; set; }    
      
        public int VendorID { get; set; }
        public List<OtherLocationList> Lista { get; set; }
    }
    public class OtherLocationList
    {
      
        public string LocationName { get; set; }
       
        public string LocationAddress { get; set; }

        public int VendorID { get; set; }
    }
}
