using System;

#nullable disable

namespace Restaurant.Data.Entities
{
    public partial class TokenLog : BaseEntity
    {
        public Guid CustomerId { get; set; }
        public string Token { get; set; }
        public bool IsActive { get; set; }
        public DateTime ExpiresAt { get; set; }
    }
}