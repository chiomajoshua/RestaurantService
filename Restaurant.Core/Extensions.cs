using Microsoft.Extensions.DependencyInjection;
using Restaurant.Data.Entities;
using TanvirArjel.EFCore.GenericRepository;

namespace Restaurant.Core
{
    public static class Extensions
    {
        public static IServiceCollection RegisterGenericRepository(this IServiceCollection serviceCollection)
        {
            serviceCollection.AddGenericRepository<RestaurantContext>();
            return serviceCollection;
        }
    }
}