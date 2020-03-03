using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CurrencyViewer.API.Authentication;
using CurrencyViewer.API.Filters;
using CurrencyViewer.Application;
using CurrencyViewer.Application.Interfaces;
using CurrencyViewer.Application.Models;
using CurrencyViewer.Application.Security;
using CurrencyViewer.Application.Services;
using CurrencyViewer.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace CurrencyViewer.API
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
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = ApiKeyAuthenticationOptions.DefaultScheme;
                options.DefaultChallengeScheme = ApiKeyAuthenticationOptions.DefaultScheme;
            })
            .AddApiKeySupport(options => { });

            services.AddAuthorization(options =>
            {
                options.AddPolicy(Policies.ReadonlyUsers, policy => policy.Requirements.Add(new ReadonlyUserRequirement()));
            });

            services.AddControllers(options => options.Filters.Add(typeof(CustomExceptionFilterAttribute)));

            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            services.AddDbContext<CurrencyDbContext>(dbOptions =>
            {
                dbOptions.UseNpgsql(connectionString);
            });

            services.Configure<CurrencyRatesConfig>(Configuration.GetSection(CurrencyRatesConfig.JsonPropertyName));
            services.AddHostedService<CurrencyRatesHostedService>();
            services.AddScoped<ICurrencyRatesReceiver, CurrencyRatesReceiver>();
            services.AddScoped<ICurrencyRatesProcessor, CurrencyRatesProcessor>();
            services.AddScoped<ICurrencyRatesQueryService, CurrencyRatesQueryService>();
            services.AddScoped<ICurrencyRatesCommandService, CurrencyRatesCommandService>();

            services.AddHttpClient();            

            services.AddSingleton<IAuthorizationHandler, ReadonlyUserAuthorizationHandler>();
            services.AddScoped<IGetApiKeyQuery, GetApiKeyQuery>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();
            
            app.UseHttpsRedirection();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
