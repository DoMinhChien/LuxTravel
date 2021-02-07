using System;
using LuxTravel.Model.BaseRepository;
using LuxTravel.Model.Entites;
using LuxTravel.Model.GenericRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace LuxTravel.Api
{
    public static class ContainerSetup
    {
        public static void Setup(IServiceCollection services, IConfiguration configuration)
        {
            AddUow(services, configuration);
            ConfigureAuth(services);
        }

        private static void ConfigureAuth(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }


        private static void AddUow(IServiceCollection services, IConfiguration configuration)
        {


            if (Environment.GetEnvironmentVariable("ENVIRONMENT_VARIABLE") == "Production")
            {
                services.AddDbContext<LuxTravelDBContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("AzureConnection"),
                        sqlServerOptions => sqlServerOptions.CommandTimeout(60)));
            }
            else
            {
                services.AddDbContext<LuxTravelDBContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection"),
                        sqlServerOptions => sqlServerOptions.CommandTimeout(60)));
            }
            services.AddScoped(typeof(BaseRepository<>));
            services.AddScoped(typeof(UnitOfWork));
        }


    }
}
