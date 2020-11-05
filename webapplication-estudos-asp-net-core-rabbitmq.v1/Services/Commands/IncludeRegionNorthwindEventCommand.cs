using webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Interfaces.Infra.Queues.RabbitMq;
using webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Interfaces.Services.Commands;
using webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Models.RabbitMq.Events;

namespace webapplication_estudos_asp_net_core_rabbitmq.v1.Services.Commands
{
    public class IncludeRegionNorthwindEventCommand : IIncludeRegionNorthwindEventCommand
    {
        private IRabbitMqInfra _rabbitMqInfra;



        public IncludeRegionNorthwindEventCommand(IRabbitMqInfra rabbitMqInfra)
        {
            _rabbitMqInfra = rabbitMqInfra;
        }



        public void Handle(IncludeRegionNorthwindEvent request)
        {
            _rabbitMqInfra.PublishMessageIncludeRegion<IncludeRegionNorthwindEvent>(request);
        }
    }
}
