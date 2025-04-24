using Microsoft.Extensions.Configuration;

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

            return Services;
        }
    }
}
