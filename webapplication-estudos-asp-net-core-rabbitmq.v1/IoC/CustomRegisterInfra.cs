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
            //docker run -d --hostname local_rabbitmq --name local_rabbitmq -p 15672:15672 -p 5672:5672 -e RABBITMQ_DEFAULT_USER=guest -e RABBITMQ_DEFAULT_PASS=guest rabbitmq:3-management
            //docker network create --driver bridge rede_local_containers
            //docker network connect --link webapplication-estudos-asp-net-core-rabbitmq.v1:c1 rede_local_containers local_rabbitmq
            //docker network connect --link local_rabbitmq:c2 rede_local_containersc webapplication-estudos-asp-net-core-rabbitmq.v1
            //docker network rm rede_local_containers
            services.Configure<RabbitMqConfiguration>(configuration.GetSection(AppSettingsConstants.SECTION_RABBIT_MQ));

            services.AddTransient<IRabbitMqInfra, RabbitMqInfra>();
        }
    }
}
