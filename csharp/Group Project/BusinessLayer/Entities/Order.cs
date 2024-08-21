using System;
using System.Collections.Generic;

namespace BusinessLayer.Entities
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public List<OrderComic> OrderLines { get; set; }
    }
}