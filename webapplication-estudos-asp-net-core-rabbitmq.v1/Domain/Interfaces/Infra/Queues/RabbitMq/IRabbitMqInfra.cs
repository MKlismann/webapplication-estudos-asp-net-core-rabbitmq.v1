namespace webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Interfaces.Infra.Queues.RabbitMq
{
    public interface IRabbitMqInfra
    {
        void PublishMessageIncludeRegion<T>(T message);

        string GetMessageIncludeRegion();
    }
}
