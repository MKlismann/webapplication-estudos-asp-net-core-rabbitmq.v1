using Microsoft.Extensions.DependencyInjection;
using webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Interfaces.Services.Commands;
using webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Interfaces.Services.Queries;
using webapplication_estudos_asp_net_core_rabbitmq.v1.Services.Commands;
using webapplication_estudos_asp_net_core_rabbitmq.v1.Services.Queries;

namespace webapplication_estudos_asp_net_core_rabbitmq.v1.IoC
{
    public static class CustomRegisterServices
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddScoped<IIncludeRegionNorthwindEventCommand, IncludeRegionNorthwindEventCommand>();
            services.AddScoped<IGetRegionNorthwindEventQuery, GetRegionNorthwindEventQuery>();
        }
    }
}
