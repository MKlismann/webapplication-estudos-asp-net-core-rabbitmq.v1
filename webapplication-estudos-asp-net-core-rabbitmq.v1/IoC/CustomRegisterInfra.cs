using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Constants;
using webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Interfaces.Infra.Queues.RabbitMq;
using webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Models.RabbitMq.Configuration;
using webapplication_estudos_asp_net_core_rabbitmq.v1.Infra.Queues.RabbitMq;

namespace webapplication_estudos_asp_net_core_rabbitmq.v1.IoC
{
    public static class CustomRegisterInfra
    {
        public static void RegisterServices(IServiceCollection services, IConfiguration configuration)
        {
            //docker run -d --hostname rabbitmq --name rabbitmq -p 6672:5672 -p 15672:15672 -e RABBITMQ_DEFAULT_USER=guest -e RABBITMQ_DEFAULT_PASS=guest rabbitmq:3-management
            services.Configure<RabbitMqConfiguration>(configuration.GetSection(AppSettingsConstants.SECTION_RABBIT_MQ));

            services.AddTransient<IRabbitMqInfra, RabbitMqInfra>();
        }
    }
}
