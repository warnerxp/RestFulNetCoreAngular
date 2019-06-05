using System;

namespace Northwind.Models
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime MyProperty { get; set; }
        public string OrderNumber { get; set; }
        public int CustomerId { get; set; }
        public decimal TotalAmount { get; set; }
    }
}










