using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MailBProductTask.Helpers;
using MailBProductTask.Installers;
using MailBProductTask.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MailBProductTask
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
            //Services
            services.InstallServicesInAssembly(Configuration);
            services.AddCors();
            services.AddControllers();

            // configure basic authentication 
            services.AddAuthentication("BasicAuthentication")
                .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthentication", null);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            // global cors policy
            app.UseCors(x => x
                .AllowAnyOrigin()
                .AllowAnyMethod()
                .AllowAnyHeader());

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}

/*
Hot Swapping Custom Configurations in ASP.net Core Using IOptionsSnapshot

https://dotnetcoretutorials.com/2017/01/01/hot-swapping-custom-configurations-asp-net-core-using-ioptionssnapshot/

Как работает конфигурация в.NET Core
https://habr.com/ru/post/453416/

Dynamic Connection String In .NET Core
https://dzone.com/articles/dynamic-connection-string-in-net-core

How to reload appsettings.json at runtime each time it changes in .NET core 1.1 console application?
https://stackoverflow.com/questions/45064140/how-to-reload-appsettings-json-at-runtime-each-time-it-changes-in-net-core-1-1
*/