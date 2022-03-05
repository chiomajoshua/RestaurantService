using System;
using System.Collections.Generic;

#nullable disable

namespace Restaurant.Data.Entities
{
    public partial class Menu : BaseEntity
    {
        public Menu()
        {
            OrderMenus = new HashSet<OrderMenu>();
        }

        public string DishName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }

        public virtual Category Category { get; set; }
        public virtual ICollection<OrderMenu> OrderMenus { get; set; }
    }
}