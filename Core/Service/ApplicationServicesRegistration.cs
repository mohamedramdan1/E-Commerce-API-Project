using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;

namespace Service
{
    public static class ApplicationServicesRegistration
    {
        // Helper Method services
        public static IServiceCollection AddApplicationServices(this IServiceCollection Services)
        {
            Services.AddAutoMapper(typeof(Service.AssemblyRefrence).Assembly);
            Services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();


            // For Factory Delegate in Service Manager
            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<Func<IProductService>>(Provider =>
            () => Provider.GetRequiredService<IProductService>());

            Services.AddScoped<IBasketService, BasketService>();
            Services.AddScoped<Func<IBasketService>>(Provider =>
            () => Provider.GetRequiredService<IBasketService>());

            Services.AddScoped<IAuthenticationService, AuthenticationService>();
            Services.AddScoped<Func<IAuthenticationService>>(Provider =>
            () => Provider.GetRequiredService<IAuthenticationService>());

            Services.AddScoped<IOrderServices, OrderService>();
            Services.AddScoped<Func<IOrderServices>>(Provider =>
            () => Provider.GetRequiredService<IOrderServices>());

            Services.AddScoped<IPaymentService,PaymentService>();
            Services.AddScoped<Func<IPaymentService>>(Provider =>
            () => Provider.GetRequiredService<IPaymentService>());

            Services.AddScoped<ICacheServices, CacheService>();
            return Services;
        }
    }
}
