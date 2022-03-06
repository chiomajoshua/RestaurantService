using System;

namespace Restaurant.Data.Models.Authentication
{
    public class CreateTokenRequest
    {
        public string Token { get; set; }
        public Guid CustomerId { get; set; }
    }
}