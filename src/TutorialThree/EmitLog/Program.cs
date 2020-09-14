using System;
using RabbitMQ.Client;
using System.Text;

namespace EmitLog
{
    internal sealed class Program
    {
        private static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "logs", type: ExchangeType.Fanout);
                var message = args.Length > 0 ? string.Join(" ", args) : "Info: Hello World!";
                var body = Encoding.UTF8.GetBytes(message);

                channel.BasicPublish(
                    exchange: "logs",
                    routingKey: string.Empty,
                    basicProperties: null,
                    body: body);
                Console.WriteLine($"[X] Sent log: {message}");
            }

            Console.WriteLine("Press [enter] to exit.");
            Console.ReadLine();
        }
    }
}
