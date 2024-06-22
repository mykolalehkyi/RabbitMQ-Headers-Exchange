using RabbitMQ.Client;
using System.Text;

internal class Program
{
    private static void Main(string[] args)
    {
        Console.Title = "Producer";
        Thread.Sleep(2000);

        var count = 0;
        Task.Run(() =>
        {
            do
            {
                int timeToSleep = new Random().Next(1000, 7000);
                Thread.Sleep(timeToSleep);
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(exchange: "headers_ex", type: ExchangeType.Headers);

                        Dictionary<string, object> headers = new Dictionary<string, object>();
                        headers.Add("currency", "usd");
                        headers.Add("transfer", "abroad");

                        var properties = channel.CreateBasicProperties();
                        properties.Headers = headers;

                        string message = $"[{count}] sent with headers: [currency:usd] [transfer:abroad]";

                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(
                            exchange: "headers_ex",
                            routingKey: "doesnt matter",
                            basicProperties: properties,
                            body: body);
                        Console.WriteLine($"[{count++} USD transfer from abroad]");
                    }
                }

            } while (true);
        });

        Task.Run(() =>
        {
            do
            {
                int timeToSleep = new Random().Next(1000, 5000);
                Thread.Sleep(timeToSleep);
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(exchange: "headers_ex", type: ExchangeType.Headers);

                        Dictionary<string, object> headers = new Dictionary<string, object>();
                        headers.Add("currency", "usd");
                        headers.Add("transfer", "internal");

                        var properties = channel.CreateBasicProperties();
                        properties.Headers = headers;

                        string message = $"[{count}] sent with headers: [currency:usd] [transfer:internal]";

                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(
                            exchange: "headers_ex",
                            routingKey: "doesnt matter",
                            basicProperties: properties,
                            body: body);
                        Console.WriteLine($"[{count++} USD transfer internal]");
                    }
                }

            } while (true);
        });

        Task.Run(() =>
        {
            do
            {
                int timeToSleep = new Random().Next(1000, 6000);
                Thread.Sleep(timeToSleep);
                var factory = new ConnectionFactory() { HostName = "localhost" };
                using (var connection = factory.CreateConnection())
                {
                    using (var channel = connection.CreateModel())
                    {
                        channel.ExchangeDeclare(exchange: "headers_ex", type: ExchangeType.Headers);

                        Dictionary<string, object> headers = new Dictionary<string, object>();
                        headers.Add("currency", "eur");
                        headers.Add("transfer", "internal");

                        var properties = channel.CreateBasicProperties();
                        properties.Headers = headers;

                        string message = $"[{count}] sent with headers: [currency:eur] [transfer:internal]";

                        var body = Encoding.UTF8.GetBytes(message);
                        channel.BasicPublish(
                            exchange: "headers_ex",
                            routingKey: "doesnt matter",
                            basicProperties: properties,
                            body: body);
                        Console.WriteLine($"[{count++} EUR transfer internal]");
                    }
                }

            } while (true);
        });

        Console.ReadKey();
    }
}