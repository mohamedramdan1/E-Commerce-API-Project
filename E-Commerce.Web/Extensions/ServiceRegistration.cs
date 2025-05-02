using System.Text;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

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

        public static IServiceCollection AddJWTService(this IServiceCollection Services, IConfiguration configuration)
        {
            Services.AddAuthentication(Config =>
            {
                Config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(Options =>
            {
                Options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = configuration["JWTOptions:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = configuration["JWTOptions:Audience"],

                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWTOptions:SecretKey"]))
                };
            });
            return Services;
        }
    }
}
