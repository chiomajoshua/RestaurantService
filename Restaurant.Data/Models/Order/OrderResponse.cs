using System;

namespace Restaurant.Data.Models.Order
{
    public class OrderResponse
    {
        public Guid Id { get; set; }
        public int Quantity { get; set; }
        public DateTimeOffset OrderDate { get; set; }
        public string BatchId { get; set; }
        public string DishName { get; set; }
    }
}