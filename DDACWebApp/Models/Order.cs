using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DDACWebApp.Models
{
    public class Order
    {
        string Name { get; set; }
        int quantity { get; set; }
        int price{ get; set; }
    DateTime OrderDate { get; set; }
        DateTime tourDate { get; set; }


    }
}