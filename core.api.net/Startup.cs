using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Swashbuckle.AspNetCore.Swagger;

using NLog;
using NLog.Extensions.Logging;
using DryIoc;

//Helper
using Core.Api.Net.Business.Helpers.Environment;

//Model
using Core.Api.Net.Business.Models.Configs;

//Service
using Core.Api.Net.Business.Services.Business;

//Repository
using Core.Api.Net.Business.Repository.Business;

namespace Core.Api.Net
{
    public class Startup
    {
        private static Logger _logger = LogManager.GetCurrentClassLogger();
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var cultureInfo = new CultureInfo("en-GB");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            EnvironmentModel EnvronmentSetting = new EnvironmentModel()
            {
                GoogleMapApi = Configuration.GetSection("GoogleMapApi").Get<GoogleMapApi>(),
            };
            string _baseUrl = Encoding.UTF8.GetString(Convert.FromBase64String(EnvronmentSetting.GoogleMapApi.BaseUrl));
            EnvronmentSetting.GoogleMapApi.BaseUrl = string.Format(_baseUrl, EnvronmentSetting.GoogleMapApi.Type, EnvronmentSetting.GoogleMapApi.ApiKey, "{0}");

            services.AddHttpClient();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Core API", Version = "v1" });
            });

            services.AddControllers();

            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

            // Register Service
            services.AddScoped<IEnvironmentConfigs>(r => new EnvironmentConfigs(EnvronmentSetting));
            services.AddScoped<IGoogleMapApiService, GoogleMapApiService>();

            // Register Repository
            services.AddScoped<IGoogleMapApiRepository, GoogleMapApiRepository>();

        }


        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "Api Core Service V.1");
            });


            //global cors policy
            app.UseCors(x => x
               .AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader());

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

        }
    }
}
