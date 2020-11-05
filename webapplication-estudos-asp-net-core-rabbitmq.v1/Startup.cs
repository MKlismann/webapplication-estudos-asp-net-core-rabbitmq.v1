using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using System;
using System.IO;
using System.Reflection;
using webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Constants;
using webapplication_estudos_asp_net_core_rabbitmq.v1.IoC;

namespace webapplication_estudos_asp_net_core_rabbitmq.v1
{
    public class Startup
    {
        public IConfiguration Configuration { get; }
        private readonly string _identificacaoAplicacao = $"{ApplicationConstants.APPLICATION_NAME} - {ApplicationConstants.APPLICATION_VERSION}";




        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }


        

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();

            services.AddOptions();

            services.AddSwaggerGen(swaggerConfiguration =>
            {
                swaggerConfiguration.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = ApplicationConstants.APPLICATION_VERSION,
                    Title = ApplicationConstants.APPLICATION_NAME,
                    Description = ApplicationConstants.APPLICATION_DESCRIPTION
                });

                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                swaggerConfiguration.IncludeXmlComments(xmlPath);
            });            

            CustomRegisterServices.RegisterServices(services);
            CustomRegisterInfra.RegisterServices(services, Configuration);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            app.UseSwagger();
            app.UseSwaggerUI(swaggerConfiguration =>
            {
                swaggerConfiguration.SwaggerEndpoint("/swagger/v1/swagger.json", _identificacaoAplicacao);
                swaggerConfiguration.RoutePrefix = string.Empty;
            });

            app.UseStaticFiles();
        }
    }
}
