using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ExecutiveRefreshment.Models
{

    public class Cart
    {
        public long ID { get; set; }
        public string Title { get; set; }
        public string  Description { get; set; }
        public string Size { get; set; }

        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}