using System;
using RabbitMQ.Client;

namespace NetCoreRabbitMq.Mensageria.Conector
{
    public static class Conector
    {
        private static ConnectionFactory factory;
        private static IConnection conn;
        public static IModel channel;

        public static void Iniciar()
        {
            ConfigurarNovaFactory();
            CriarConexao();
            CriarChannel();
        }

        public static void ConfigurarNovaFactory()
        {
            factory = new ConnectionFactory();
            // "guest"/"guest" by default, limited to localhost connections
            factory.UserName = "*****";
            factory.Password = "*****";
            factory.VirtualHost = "hostbetologs";
            factory.HostName = "xx.xx.xx.xx";
        }

        public static void CriarConexao()
        {
            conn = factory.CreateConnection();
        }
        
        public static void CriarChannel()
        {
            channel = conn.CreateModel();
        }

        public static void Fechar()
        {
            channel.Close(200, "Goodbye");
            conn.Close();
        }
    }
}