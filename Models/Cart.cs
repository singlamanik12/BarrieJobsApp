using System;
using System.Collections.Generic;

namespace BarrieJobsApp.Models
{
    public partial class Cart
    {
        public string CartId { get; set; }
        public int JobId { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }
        public string Username { get; set; }
    }
}
