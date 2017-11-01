using System;
using System.Threading;
using NetCoreRabbitMq.Mensageria.Conector;
using NetCoreRabbitMq.Mensageria.Publicador;
using NetCoreRabbitMq.Mensageria.Leitor;

namespace NetCoreRabbitMq
{
    class Program
    {
        static void Main(string[] args)
        {
            Conector.Iniciar();
            var publicador = new Publicador(Conector.channel);
            var leitor = new Leitor(Conector.channel);
            //bool continuar = true;

            var exchange = "betologs";
            var queueName = "Testebeto";
            byte[] mensagem;

            for (int i = 0; i < 500; i++)
            {
                mensagem = publicador.CriarMensagem(string.Format("Mensagem de teste {0}!",i));
                publicador.EnviarMensagem(exchange,queueName,mensagem);
            }
            
            Console.WriteLine("Mensagens publicada no Rabbit!");

            //leitor.LerTodaFilaSemRetorno(queueName);
            //leitor.CriarConsumidor(queueName);

            //Console.WriteLine("Consumidor inicializado, aguardando mensagens");
            //Console.Read();

            //Console.WriteLine("Mensagens lidas do Rabbit!");

            //leitor.FecharConsumidor();
            Conector.Fechar();
        }
    }
}
