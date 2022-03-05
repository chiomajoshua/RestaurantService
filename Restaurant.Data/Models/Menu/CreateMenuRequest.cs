using System;

namespace Restaurant.Data.Models.Menu
{
    public class CreateMenuRequest
    {
        public string DishName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public Guid CategoryId { get; set; }
    }
}