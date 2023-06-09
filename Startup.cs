// --------------------------------------------------------------- 
// Copyright (c)  the Gulchexra Burxonova
// Talent Management 
// ---------------------------------------------------------------

using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.OData;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using TalentManagement.Core.Brokers.DateTimes;
using TalentManagement.Core.Brokers.Emails;
using TalentManagement.Core.Brokers.Loggings;
using TalentManagement.Core.Brokers.Storages;
using TalentManagement.Core.Brokers.Tokens;
using TalentManagement.Core.Services.Foundations.Emails;
using TalentManagement.Core.Services.Foundations.Securities;
using TalentManagement.Core.Services.Foundations.Users;
using TalentManagement.Core.Services.Orchestrations;
using TalentManagement.Core.Services.Processings.Users;
using Tarteeb.Api.Brokers.Tokens;

namespace TalentManagement.Core
{
    public class Startup
    {
        public Startup(IConfiguration configuration) =>
            Configuration = configuration;

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddControllers().AddOData(options => options.Select().Filter().OrderBy());
            services.AddDbContext<StorageBroker>();
            services.AddCors(option =>
            {
                option.AddPolicy("MyPolicy", config =>
                {
                    config.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });
            RegisterBrokers(services);
            AddFoundationServices(services);
            AddProcessingServices(services);
            AddOrchestrationServices(services);
            RegisterJwtConfigurations(services, Configuration);

            services.AddSwaggerGen(config =>
            {
                config.SwaggerDoc("v1", new OpenApiInfo { Title = "TalentManagement.Core", Version = "v1" });

                config.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    In = ParameterLocation.Header,
                    Description = "Please enter token",
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    BearerFormat = "JWT",
                    Scheme = "bearer"
                });

                config.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type=ReferenceType.SecurityScheme,
                                Id="Bearer"
                            }
                        },
                        new string[]{}
                    }
                });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment environment)
        {
            if (environment.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(config => config.SwaggerEndpoint(
                    "/swagger/v1/swagger.json",
                    "TalentManagement.Core v1"));
            }

            app.UseHttpsRedirection();
            app.UseRouting();
            app.UseCors("MyPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
                endpoints.MapControllers());
        }

        private static void RegisterBrokers(IServiceCollection services)
        {
            services.AddTransient<IStorageBroker, StorageBroker>();
            services.AddTransient<ILoggingBroker, LoggingBroker>();
            services.AddTransient<IDateTimeBroker, DateTimeBroker>();
            services.AddTransient<ITokenBroker, TokenBroker>();
            services.AddTransient<IEmailBroker, EmailBroker>();
        }

        private static void AddFoundationServices(IServiceCollection services)
        {
            services.AddTransient<IUserService, UserService>();
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<IEmailService, EmailService>();
        }

        private static void AddProcessingServices(IServiceCollection services) =>
            services.AddTransient<IUserProcessingService, UserProcessingService>();

        private static void AddOrchestrationServices(IServiceCollection services) =>
            services.AddTransient<IUserSecurityOrchestrationService, UserSecurityOrchestrationService>();

        private static void RegisterJwtConfigurations(IServiceCollection services,
            IConfiguration configuration)
        {
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    string key = configuration.GetSection("Jwt").GetValue<string>("Key");
                    byte[] convertKeyToBytes = Encoding.UTF8.GetBytes(key);

                    options.TokenValidationParameters = new TokenValidationParameters()
                    {
                        ValidateIssuerSigningKey = true,
                        IssuerSigningKey = new SymmetricSecurityKey(convertKeyToBytes),
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        RequireExpirationTime = true,
                        ValidateLifetime = true
                    };
                });
        }
    }
}