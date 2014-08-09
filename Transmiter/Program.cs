using System;
using System.Text;
using RabbitMQ.Client;

namespace Transmiter
{
    class Program
    {
        static void Main(string[] args)
        {
            var factory = new ConnectionFactory() { HostName = "localhost" };

            using (var connection = factory.CreateConnection())
            {
                using (var channel = connection.CreateModel())
                {
                    channel.QueueDeclare("hello", false, false, false, null);

                    while (true)
                    {
                        string message = Console.ReadLine();

                        if (!string.IsNullOrEmpty(message))
                        {
                            var body = Encoding.UTF8.GetBytes(message);

                            channel.BasicPublish("", "hello", null, body);

                            Console.WriteLine(" [x] Sent {0}", message);
                        }
                        else
                        {
                            Console.WriteLine("Empty string. Message was not queued.");
                        }
                    }
                }
            }
        }
    }
}
