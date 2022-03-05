using System;

namespace Restaurant.Data.Models.Order
{
    public class OrderRequest
    {
        public int Quantity { get; set; }        
        public Guid MenuId { get; set; }
    }

    public class CreateOrderRequest
    {
        public OrderRequest[] OrderRequest { get; set; }
        public string Status { get; set; }
        public Guid CustomerId { get; set; }
    }
}