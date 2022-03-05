using System.Collections.Generic;

#nullable disable

namespace Restaurant.Data.Entities
{
    public partial class Category : BaseEntity
    {
        public Category()
        {
            Menus = new HashSet<Menu>();
        }

        public string Name { get; set; }
        public string Description { get; set; }

        public virtual ICollection<Menu> Menus { get; set; }
    }
}