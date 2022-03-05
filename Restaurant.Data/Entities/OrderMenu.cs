using System;

#nullable disable

namespace Restaurant.Data.Entities
{
    public partial class OrderMenu
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public Guid? OrderId { get; set; }
        public Guid? MenuId { get; set; }

        public virtual Menu Menu { get; set; }
        public virtual Order Order { get; set; }
    }
}