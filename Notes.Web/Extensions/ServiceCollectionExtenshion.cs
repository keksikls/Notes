using Microsoft.EntityFrameworkCore;
using Notes.Data.AppDbContext;
using Notes.Data.UnitOfWorks;
using Notes.Data.UnitOfWorks.IUnitOfWorks;
using Serilog;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.DependencyInjection;

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
        //регистрируем в di время жизни для обьекта unitofwork
        public static WebApplicationBuilder AddApplicationServices(this WebApplicationBuilder builder)
        {
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            return builder;
        }

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
    }
}
