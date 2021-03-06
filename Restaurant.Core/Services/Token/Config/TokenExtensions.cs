using Restaurant.Data.Models.Authentication;
using System;

namespace Restaurant.Core.Services.Token.Config
{
    public static class TokenExtensions
    {
        public static Data.Entities.TokenLog ToDbToken(this CreateTokenRequest createTokenRequest)
        {
            return new Data.Entities.TokenLog
            {
                Token = createTokenRequest.Token,
                CustomerId = createTokenRequest.CustomerId
            };
        }
    }
}