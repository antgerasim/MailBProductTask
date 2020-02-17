using MailBProductTask.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.DataAnnotations;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MailBProductTask.Installers
{
    public class ServiceInstaller : IInstaller
    {
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IClientService, ClientService>();
            services.AddScoped<IProductService, ProductService>();
            services.AddScoped<INameValidator, NameValidator>();
            services.AddScoped<IDescriptionValidator, DescriptionValidator>();

            services.AddHttpContextAccessor();
        }
    }
}

//IValidationAttributeAdapterProvider,CustomValidationAttributeAdapterProvider