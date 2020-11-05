using webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Models.RabbitMq.Events;

namespace webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Interfaces.Services.Commands
{
    public interface IIncludeRegionNorthwindEventCommand
    {
        void Handle(IncludeRegionNorthwindEvent request);
    }
}
