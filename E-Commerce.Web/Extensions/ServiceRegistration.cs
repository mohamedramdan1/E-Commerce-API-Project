using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Web.Extensions
{
    public static class ServiceRegistration
    {
        // Helper Method services

        public static IServiceCollection AddSwaggerServices(this IServiceCollection Services)
        {
            Services.AddEndpointsApiExplorer(); // For Swagger
            Services.AddSwaggerGen();// For Swagger
            return Services;
        }

        public static IServiceCollection AddWebApplicationsServices(this IServiceCollection Services)
        {
            Services.Configure<ApiBehaviorOptions>((Options =>
            {
                Options.InvalidModelStateResponseFactory = ApiResponseFactories.GenerateApiValoidationErrorResponse;
            }));
            return Services;
        }
    }
}
