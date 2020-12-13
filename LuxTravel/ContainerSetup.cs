using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using LuxTravel.Models.Entities;
using LuxTravel.Models.GenericRepository;
using LuxTravel.Models.GenericRepository.Interfaces;
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
            AddQueries(services);
            ConfigureAuth(services);
        }

        private static void ConfigureAuth(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }


        private static void AddUow(IServiceCollection services, IConfiguration configuration)
        {

            if (Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") == "Production")
            {
                services.AddDbContext<LuxTravelContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("AzureConnection")));
            }
            else
            {
                services.AddDbContext<LuxTravelContext>(options =>
                    options.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));
            }


            services.BuildServiceProvider().GetService<LuxTravelContext>().Database.Migrate();


            services.AddScoped(typeof(IBaseRepository<,>), typeof(BaseRepository<,>));

            services.AddScoped<IUnitOfWork>(ctx => new UnitOfWork(ctx.GetRequiredService<LuxTravelContext>()));
        }

        private static void AddQueries(IServiceCollection services)
        {

            //var exampleProcessorType = typeof(BaseRepository<,>);
            //var types = (from t in exampleProcessorType.GetTypeInfo().Assembly.GetTypes()
            //             where t.Namespace == exampleProcessorType.Namespace
            //                 && t.GetTypeInfo().IsClass
            //                 && t.GetTypeInfo().GetCustomAttribute<CompilerGeneratedAttribute>() == null
            //             select t).ToArray();

            //foreach (var type in types)
            //{
            //    var interfaceQ = type.GetTypeInfo().GetInterfaces().First();
            //    services.AddScoped(interfaceQ, type);
            //}
        }
    }
}
