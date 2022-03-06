using System;
using System.Collections.Generic;

namespace Restaurant.Data.Models.Order
{
    public class OrderRequest
    {
        public int Quantity { get; set; }
        public Guid MenuId { get; set; }
    }

    public class CreateOrderRequest
    {
        public List<OrderRequest> OrderRequests { get; set; }
        public Guid CustomerId { get; set; }
    }
}