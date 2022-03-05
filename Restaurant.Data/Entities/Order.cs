using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurant.Data.Entities
{
    public partial class Order : BaseEntity
    {
        public Order()
        {
            OrderMenus = new HashSet<OrderMenu>();
        }

        public int Quantity { get; set; }
        public Guid CustomerId { get; set; }
        public string BatchId { get; set; }

        public virtual Customer Customer { get; set; }
        public virtual ICollection<OrderMenu> OrderMenus { get; set; }
    }
}