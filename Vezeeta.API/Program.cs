using Vezeeta.API.Middlwares;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Vezeeta.Repository.Data;
using Vezeeta.API.Extensions;
using Vezeeta.Core.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Vezeeta.Core.Domain.Enums;

namespace Vezeeta.API
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            #region Configure Services
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddSwaggerDocumentaion();

            // DB Of Vezeeta
            builder.Services.AddDbContext<AppDbContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
            });

            // Call Method Which Contain All Services
            builder.Services.AddApplicationServices();

            // Call Method Which Contain All IdentityServices
            builder.Services.AddIdentityServices(builder.Configuration);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy("MyPolicy", options =>
                {
                    options.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin().SetIsOriginAllowed(origin => true); ;
                });
            });
            #endregion

            var app = builder.Build();

            // using => to make dispose
            using var scope = app.Services.CreateScope();

            var services = scope.ServiceProvider;

            // Create Object from ILggerFactory
            var loggerFactory = services.GetRequiredService<ILoggerFactory>();

            try
            {
                // Ask Explicilty
                // Create Object From AppDbContext
                var appDbContext = services.GetRequiredService<AppDbContext>();
                await appDbContext.Database.MigrateAsync(); // Update-Database
                await DbContextSeed.SeedAsync(appDbContext);

                // Seed Admin
                IdentitySeed.SeedUserAsync(services);
            }
            catch (Exception ex)
            {

                // Create Object from ILogger
                var logger = loggerFactory.CreateLogger<Program>();
                logger.LogError(ex, "an error occured during apply the migration");
            }

            #region Configure Kestral Middlewares
            app.UseMiddleware<ExceptionMiddleware>();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            //It has relationship with ErrorsController.
            app.UseStatusCodePagesWithReExecute("/errors/{0}");

            app.UseHttpsRedirection();

            app.UseStaticFiles();

            app.UseCors("MyPolicy");

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();
            #endregion

            app.Run();
        }
    }
}