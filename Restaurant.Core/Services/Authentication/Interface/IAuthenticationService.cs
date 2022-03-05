using Restaurant.Core.Helpers.Autofac;
using Restaurant.Data.Models.Authentication;
using System.Threading.Tasks;

namespace Restaurant.Core.Services.Authentication.Interface
{
    public interface IAuthenticationService : IAutoDependencyCore
    {
        /// <summary>
        /// Gain Entry to Site
        /// </summary>
        /// <param name="loginRequest"></param>
        /// <returns></returns>
        Task<bool> Login(LoginRequest loginRequest);
    }
}