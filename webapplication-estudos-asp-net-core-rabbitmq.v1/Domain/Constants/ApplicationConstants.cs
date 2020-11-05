using System.Globalization;

namespace webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Constants
{
    public static class ApplicationConstants
    {
        public readonly static CultureInfo CULTURE_INFO_PT_BR = new CultureInfo("pt-BR");
        public const string APPLICATION_NAME = "webapplication-estudos-asp-net-core-rabbitmq";
        public const string APPLICATION_VERSION = "v1";
        public const string APPLICATION_DESCRIPTION = "Estudos .NET Core WEB API + RabbitMQ + Docker + etc...";
    }
}
