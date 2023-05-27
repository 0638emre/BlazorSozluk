using System.Text;
using System.Text.Json;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace BlazorSozluk.Common.Infrastructure
{
    public static class QueueFactory
    {
        public static void SendMessageToExchange(string exchangeName, 
                                        string exchangeType, 
                                        string queueName, 
                                        object obj)
        {
            //önce rabbitmq client npm indirmemiz gerek.

            var channel = CreateBasicConsumer().EnsureExchange(exchangeName, exchangeType).EnsureQueue(queueName, exchangeName).Model;
            var body = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(obj));

            channel.BasicPublish(exchange:exchangeName, routingKey:queueName, basicProperties:null, body:body);

        }

        public static EventingBasicConsumer CreateBasicConsumer()
        {
            //RabbitMQ sunucusundan mesajları tüketmek için kullanılan bir tüketici sınıfıdır. Bu sınıf, RabbitMQ'ya abone olur ve belirli bir kuyruğu dinleyerek gelen mesajları işleyebilir.
            var factory = new ConnectionFactory() { HostName = SozlukConstans.RabbitMQHost, UserName = "emre", Password = "123456"};
            var connection = factory.CreateConnection();
            var channel = connection.CreateModel();

            return new EventingBasicConsumer(channel);
        }

        public static EventingBasicConsumer EnsureExchange(this EventingBasicConsumer consumer, 
                                                            string exchangeName, 
                                                            string exchangeType = SozlukConstans.DefaultExchangeType)
        {
            consumer.Model.ExchangeDeclare(exchange:exchangeName,
                                            type:exchangeType,
                                            durable:false,
                                            autoDelete: false);

            //burada exchange in yaratılmış olduğundan emin oluyoruz.

            return consumer;
        }

        public static EventingBasicConsumer EnsureQueue(this EventingBasicConsumer consumer,
                                                            string queueName,
                                                            string exchangeName)
        {
            consumer.Model.QueueDeclare(queue:queueName,
                durable:false,
                exclusive:false,
                autoDelete:false,
                null);

            consumer.Model.QueueBind(queueName, exchangeName, queueName);

            //burada queue in yaratılmış olduğundan emin oluyoruz.

            return consumer;
        }
    }
}
