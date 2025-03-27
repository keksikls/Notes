using Microsoft.EntityFrameworkCore;
using Notes.Data.AppDbContext;
using Notes.Data.UnitOfWorks;
using Notes.Data.UnitOfWorks.IUnitOfWorks;
using Serilog;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Identity.UI.Services;
using IdentityService.AuthEmail;
using Microsoft.OpenApi.Models;
using Service.CategoryServices.ICategoryServices;
using Service.CategoryServices;

namespace Notes.Web.Extensions
{
    public static class ServiceCollectionExtenshion
    {
        public static WebApplicationBuilder AddData(this WebApplicationBuilder builder)
        {
            builder.Services.AddDbContext<ApplicationDbContext>(opt =>
                opt.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

            return builder;
        }
        //регистрируем в di время жизни для обьекта 
        public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddSingleton<IEmailSender, EmailSender>();
            builder.Services.AddScoped<ICategoryService, CategoryService>();

            return builder;
        }
        //logs
        public static IServiceCollection AddCustomLogging(this IServiceCollection services) 
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .WriteTo.File("logs/app.log", rollingInterval: RollingInterval.Day)
                .CreateLogger();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.ClearProviders();
                loggingBuilder.AddSerilog();
            });

            return services;
        }

        //настройка реги
        public static IServiceCollection AdIdentity(this IServiceCollection services) 
        {
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders();

            return services;
        }

        public static IServiceCollection AddConfigureCoockie(this IServiceCollection services) 
        {
            services.ConfigureApplicationCookie(opt => 
            {
                opt.LoginPath = $"/Identity/Account/Login";
                opt.LogoutPath = $"/Identity/Account/Logout";
                opt.AccessDeniedPath = $"/Identity/Account/AccessDenied";
            });

            return services;
        }

        public static WebApplicationBuilder AddSwagger(this WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen();
            return builder;
        }
    }
}
