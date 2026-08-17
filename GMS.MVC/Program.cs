using Domin.Contract;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using Presistence.Repositories;
using Services.Abstraction.Contract;
using Services.Implmentations;
using Services.Mapping;

namespace GMS.MVC {
    public class Program {
        public static async Task Main(string[] args) {

            var builder = WebApplication.CreateBuilder(args);

            #region Add Services
            // Add services to the container.
            builder.Services.AddControllersWithViews();

            // Database Configuration
            builder.Services.AddDbContext<GymDbContext>(options => {
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnections"));
            });

            // Allow DI To DbInitilazer 
            builder.Services.AddScoped<IDbInitilazer, DbInitilazer>();

            // Allow DI To UnitOfWork 
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Allow DI To AnalyticsService
            builder.Services.AddScoped<IServiceManger, ServiceManger>();


            // Add AutoMapper To Services
            builder.Services.AddAutoMapper(M => M.AddProfile(new MemberProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new PlanProfile()));
            builder.Services.AddAutoMapper(M => M.AddProfile(new SessionProfile()));

            #endregion

            var app = builder.Build();

            #region Add Kestrel Middelware

            // Database Initilaizer
            using var scope = app.Services.CreateScope();
            var DbInitilaizer = scope.ServiceProvider.GetRequiredService<IDbInitilazer>();
            await DbInitilaizer.InitilazeAsync();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment()) {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"); 
            #endregion

            app.Run();
        }
    }
}

// 1. Get Into Solution Folder
// Get-ChildItem -Recurse | Unblock-File
// dotnet clean
// dotnet build