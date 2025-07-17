using DomainLayer.Models.IdentityModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Persistence.Identity;
using StackExchange.Redis;

namespace Persistence
{
    public static class InfrastructureServicesRegistration
    {
        // Helper Method services
        public static IServiceCollection AddInfrastuctureServices(this IServiceCollection Services , IConfiguration Configuration)
        {
            Services.AddDbContext<StoreDbContext>(Options =>
            {
                Options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"));
            });
            Services.AddScoped<IDataSeeding, DataSeeding>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddScoped<ICachRepository, CacheRepository>();
            Services.AddSingleton<IConnectionMultiplexer>( (_) =>
            {
                return  ConnectionMultiplexer.Connect(Configuration.GetConnectionString("RedisConnectionString"));
            });
            Services.AddDbContext<StoreIdentityDbContext>(Options =>
            {
                Options.UseSqlServer(Configuration.GetConnectionString("IdentityConnection"));
            });

            Services.AddIdentityCore<ApplicationUser>()
                     .AddRoles<IdentityRole>()
                     .AddEntityFrameworkStores<StoreIdentityDbContext>();
            return Services;
        }
    }
}
