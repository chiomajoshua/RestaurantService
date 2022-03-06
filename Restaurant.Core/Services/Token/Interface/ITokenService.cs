using Restaurant.Core.Helpers.Autofac;
using System;
using System.Threading.Tasks;

namespace Restaurant.Core.Services.Token.Interface
{
    public interface ITokenService : IAutoDependencyCore
    {
        /// <summary>
        /// Create Token
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<string> CreateToken(Guid customerId);

        /// <summary>
        /// Validate Token
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        Task<bool> ValidateToken(string token);

        /// <summary>
        /// Kills Token During Logout
        /// </summary>
        /// <param name="customerId"></param>
        /// <returns></returns>
        Task<bool> DestroyToken(string customerId);
    }
}