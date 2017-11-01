using System;
using System.IO;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace NetCoreRabbitMq.Mensageria.Leitor
{
    public class Leitor
    {
        private IModel channel;
        private String consumerTag;

        public Leitor(IModel channel)
        {
            this.channel = channel;
        }

        public void LerTodaFilaSemRetorno(string queueName)
        {
            bool noAck = false;
            bool temMensagem = true;
            while(temMensagem)
            {
                BasicGetResult result = channel.BasicGet(queueName, noAck);
                if (result == null) {
                    // No message available at this time.
                    Console.WriteLine("Sem mensagens!");
                    temMensagem=false;
                } else {
                    IBasicProperties propsRetorno = result.BasicProperties;
                    byte[] body = result.Body;
                    
                    Console.WriteLine(Encoding.UTF8.GetString(body));
                    
                    // acknowledge receipt of the message
                    channel.BasicAck(result.DeliveryTag, false);
                }
            }
        }
        
        public void CriarConsumidor(string queueName)
        {
            var consumer = new EventingBasicConsumer(channel);
            
            consumerTag = channel.BasicConsume(queueName, false, consumer);

            consumer.Received += (ch, ea) =>
                        {
                            var body = ea.Body;
                            var message = Encoding.UTF8.GetString(body);
                            var routingKey = ea.RoutingKey;
                            var messageToLog = $" [x] Recebida '{routingKey}':'{message}'";                        


                            // ... process the message
                            Console.WriteLine(messageToLog);

                            channel.BasicAck(ea.DeliveryTag, false);
                        };
        }

        public void FecharConsumidor()
        {
            channel.BasicCancel(consumerTag);
        }
    }
}