using System;
using System.Collections.Generic;
using DataAccessLayer.Models;

namespace DataAccessLayer.Models
{
    public class Order
    {
        public int Id { get; set; }

        public DateTime OrderDate { get; set; }

        public int CustomerId { get; set; }
        public Customer Customer { get; set; } = null!;

        public ICollection<Product> Products { get; set; } = new List<Product>();
    }
}