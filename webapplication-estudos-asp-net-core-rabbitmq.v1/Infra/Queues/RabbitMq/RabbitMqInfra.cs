using Microsoft.Extensions.Options;
using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Text;
using webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Interfaces.Infra.Queues.RabbitMq;
using webapplication_estudos_asp_net_core_rabbitmq.v1.Domain.Models.RabbitMq.Configuration;

namespace webapplication_estudos_asp_net_core_rabbitmq.v1.Infra.Queues.RabbitMq
{
    public class RabbitMqInfra : IRabbitMqInfra
    {
        private readonly ConnectionFactory _connectionFactory;
        private readonly string _includeRegionNorthwindQueueName;



        private IConnection _connection { get; set; }



        public RabbitMqInfra(IOptions<RabbitMqConfiguration> rabbitMqOptions)
        {
            _includeRegionNorthwindQueueName = rabbitMqOptions.Value.IncludeRegionNorthwindQueueName;
            _connectionFactory = new ConnectionFactory
            {
                HostName = rabbitMqOptions.Value.Hostname,
                UserName = rabbitMqOptions.Value.UserName,
                Password = rabbitMqOptions.Value.Password
                //VirtualHost = rabbitMqOptions.Value.VirtualHost,
                //Port = Convert.ToInt32(rabbitMqOptions.Value.Port)
            };
        }

        public void PublishMessageIncludeRegion<T>(T message)
        {
            if (_connection == null
                || !_connection.IsOpen)
            {
                _connection = CreateConnection();
            }

            using (_connection)
            {
                QueueDeclare(_includeRegionNorthwindQueueName);

                using (var channel = _connection.CreateModel())
                {
                    var exchange = string.Empty;

                    var basicProperties = channel.CreateBasicProperties();
                    basicProperties.Persistent = true;

                    var serializedMessage = JsonConvert.SerializeObject(message);
                    channel.BasicPublish(exchange, _includeRegionNorthwindQueueName, basicProperties, Encoding.UTF8.GetBytes(serializedMessage));
                }
            }
        }

        public string GetMessageIncludeRegion()
        {
            if (_connection == null
                || !_connection.IsOpen)
            {
                _connection = CreateConnection();
            }

            BasicGetResult message;

            using (_connection)
            {
                QueueDeclare(_includeRegionNorthwindQueueName);

                using (var channel = _connection.CreateModel())
                {
                    message = channel.BasicGet(_includeRegionNorthwindQueueName, true);
                }
            }

            return (message != null)
                ? Encoding.UTF8.GetString(message.Body.ToArray())
                : null;
        }

        private IConnection CreateConnection()
        {
            return _connectionFactory.CreateConnection();
        }

        private QueueDeclareOk QueueDeclare(string queueName)
        {
            QueueDeclareOk queueDeclared;

            using (var channel = _connection.CreateModel())
            {
                queueDeclared = channel.QueueDeclare(queueName, false, false, false, null);
            }

            return queueDeclared;
        }
    }
}
