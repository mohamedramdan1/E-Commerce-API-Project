
using DomainLayer.Contracts;
using E_Commerce.Web.CustomMiddleWares;
using E_Commerce.Web.Extensions;
using E_Commerce.Web.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Persistence;
using Persistence.Data;
using Persistence.Repositories;
using Service;
using ServiceAbstraction;
using Shared.ErrorModels;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container
            builder.Services.AddControllers();            
            builder.Services.AddSwaggerServices();
            builder.Services.AddInfrastuctureServices(builder.Configuration);
            builder.Services.AddApplicationServices();
            builder.Services.AddWebApplicationsServices();
            builder.Services.AddJWTService(builder.Configuration);
            
            #endregion

            var app = builder.Build();

            #region DataSeeding
            await app.SeedDataBaseAsync();
            #endregion

            #region Configure the HTTP request pipeline

            app.UseCustomExceptionmiddelWare();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwaggerModdelWare();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            #endregion
            
            app.Run();  
        }
    }
}
 