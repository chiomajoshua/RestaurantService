using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Restaurant.Data.Entities;

namespace Restaurant.Data.Extensions
{
    public static class Extensions
    {
        public static IServiceCollection RegisterDatabaseService(this IServiceCollection serviceCollection, IConfiguration configuration)
        {
            var connectionString = configuration.GetConnectionString("DefaultConnection");
            serviceCollection.AddDbContext<RestaurantContext>(options => options.UseSqlServer(connectionString));
            return serviceCollection;
        }
    }
}