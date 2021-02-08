using System.Collections.Generic;
using System.Reflection;
using System.Text;
using AutoMapper;
using CloudinaryDotNet;
using CommonFunctionality.Core.Behaviors;
using LuxTravel.Api.Core.Service;
using LuxTravel.Api.Core.Services;
using LuxTravel.Api.Mappings;
using LuxTravel.Model;
using MediatR;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Server.Kestrel.Core;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

namespace LuxTravel.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {

            var mappingConfig = new MapperConfiguration(mc => { mc.AddProfile(new ProjectMapping()); });

            IMapper mapper = mappingConfig.CreateMapper();
            services.AddSingleton(mapper);
            services.AddMvc(option => option.EnableEndpointRouting = false);
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
            services.AddCors(o => o.AddPolicy("EnableCors", builder =>
            {
                builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader();
            }));
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Lux Travel API", Version = "version 1" });
                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Description = @"JWT Authorization header using the Bearer scheme. \r\n\r\n 
                      Enter 'Bearer' [space] and then your token in the text input below.
                      \r\n\r\nExample: 'Bearer 12345abcdef'",
                    Name = "Authorization",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                c.AddSecurityRequirement(new OpenApiSecurityRequirement()
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,

                        },
                        new List<string>()
                    }
                });
            });
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false
                    };
                });

            services.Configure<CloudinarySettings>(Configuration.GetSection("CloudinarySettings"));
            services.AddControllers();
            services.AddMediatR(Assembly.GetExecutingAssembly());
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IBookingService, BookingService>();
            services.AddScoped(typeof(RequestHandlerBase));
            services.AddScoped(typeof(Cloudinary));
            ContainerSetup.Setup(services, Configuration);
            // If using Kestrel:
            services.Configure<KestrelServerOptions>(options =>
            {
                options.AllowSynchronousIO = true;
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            app.UseCors("EnableCors");
            app.UseExceptionHandler("/error");
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "LuxTravel API");
            });

            app.UseCors(options => options.AllowAnyOrigin());
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseHttpsRedirection();
            app.UseMvc();
        }
    }
}
