using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDACWebApp.Models
{
    public class Order
    {
        public string Name { get; set; }
        public int quantity { get; set; }
        public int price{ get; set; }
        public DateTime OrderDate { get; set; }
        public string tourDate { get; set; }


    }
}