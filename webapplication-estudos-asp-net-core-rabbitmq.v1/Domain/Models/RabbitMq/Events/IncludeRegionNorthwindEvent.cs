using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Models.RabbitMq.Events
{
    public class IncludeRegionNorthwindEvent : EventBase
    {
        [DefaultValue("-1")]
        public string TransactionId { get; set; }


        [Required]
        public string RegionDescription { get; set; }
    }
}
