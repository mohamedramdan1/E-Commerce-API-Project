
using System.Text.Json;
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
using Swashbuckle.AspNetCore.SwaggerUI;

namespace E_Commerce.Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            #region Add services to the container
            builder.Services.AddControllers(); 
            builder.Services.AddCors(Option=>
            {
                Option.AddPolicy("AllowAll", builder =>
                {
                    builder.AllowAnyHeader();
                    builder.AllowAnyMethod();
                    builder.AllowAnyOrigin();
                });
            });
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
                app.UseSwagger();
                app.UseSwaggerUI(Options =>
                {
                    Options.ConfigObject = new ConfigObject()
                    {
                        DisplayRequestDuration = true // Time Of Request 
                    };

                    Options.DocumentTitle = "My E-Commerce API";// Tittle in swagger

                    Options.JsonSerializerOptions = new JsonSerializerOptions()
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase// allow CameCase In Jason 
                    };

                    Options.DocExpansion(DocExpansion.None);

                    Options.EnableFilter();// Search in Swagger

                    Options.EnablePersistAuthorization();// For Authorization
                });
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseCors("AllowAll");
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();
            #endregion
            
            app.Run();  
        }
    }
}
 