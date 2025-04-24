using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleWares;

namespace E_Commerce.Web.Extensions
{
    // Helper Method (ExtentionMethod)
    public static class WebApplicationRegistration
    {
        // For Data Seeding
        public static async Task SeedDataBaseAsync(this WebApplication app)
        {
            using var Scoope = app.Services.CreateScope();
            var ObjectOfDataSeeding = Scoope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectOfDataSeeding.DataSeedAsync();
        }

        // For Custom MiddelWare
        public static IApplicationBuilder UseCustomExceptionmiddelWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
             
            return app;
        }

        public static IApplicationBuilder UseSwaggerModdelWare(this IApplicationBuilder app)
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            return app;
        }
    }
}
