using System;
using RabbitMQ.Client;

namespace NetCoreRabbitMq.Mensageria.Publicador
{
    public class Publicador
    {
        private IBasicProperties props;
        private IModel channel;
        public Publicador(IModel channel)
        {
            this.channel = channel;
            this.props = channel.CreateBasicProperties();
            props.ContentType = "text/plain";
            props.DeliveryMode = 2;
        }

        public byte[] CriarMensagem(string mensagem)
        {
            return System.Text.Encoding.UTF8.GetBytes(mensagem); 
        }
            
        public void EnviarMensagem(string exchange, string queueName, byte[] messageBodyBytes)
        {
            channel.BasicPublish(exchange,queueName, props,messageBodyBytes);
        }        
    }
}