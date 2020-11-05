using webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Constants;
using webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Interfaces.Infra.Queues.RabbitMq;
using webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Interfaces.Services.Queries;
using webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Resources;

namespace webapplication_estudos_asp_net_core_rabbitmq.v1.Services.Queries
{
    public class GetRegionNorthwindEventQuery : IGetRegionNorthwindEventQuery
    {
        private IRabbitMqInfra _rabbitMqInfra;





        public GetRegionNorthwindEventQuery(IRabbitMqInfra rabbitMqInfra)
        {
            _rabbitMqInfra = rabbitMqInfra;
        }



        public string Handle()
        {
            var retrievedMessage = _rabbitMqInfra.GetMessageIncludeRegion();

            var formattedMessage = string.Format(ApplicationConstants.CULTURE_INFO_PT_BR, Messages.MENSAGEM_RECUPERADA, retrievedMessage);
            
            return formattedMessage;
        }
    }
}
