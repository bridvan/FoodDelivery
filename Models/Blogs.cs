using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Blogs
    {
        public int Id { get; set; }
        public string Heading { get; set; }
        public string ImagesUrl { get; set; }
        public string OtherImagesUrl { get; set; }
        public string Description { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
    }
}
