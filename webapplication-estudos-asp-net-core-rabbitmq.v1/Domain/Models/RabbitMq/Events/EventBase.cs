using System;

namespace webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Models.RabbitMq.Events
{
    public abstract class EventBase
    {
        public Guid Id { get; }
        public DateTime CreatedAt { get; }



        protected EventBase()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }
    }
}
