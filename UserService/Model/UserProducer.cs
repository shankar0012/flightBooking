using Newtonsoft.Json;
using RabbitMQ.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UserService.Model
{
    public  class UserProducer
    {
        public static void FlightPublish(string QueueName,string PNR)
        {
            try
            {
                var Factory = new ConnectionFactory
                {
                    Uri = new System.Uri("amqp://guest:guest@localhost:5672")
                };
                var connection = Factory.CreateConnection();
                var channel = connection.CreateModel();

                channel.QueueDeclare(QueueName, durable: true,
                    exclusive: false, autoDelete: false,
                    arguments: null

                    );
                // var count = 10;
                //var message = new { PNRNO =  PNR };
                var body = Encoding.UTF8.GetBytes(PNR);
                channel.BasicPublish("", QueueName, null, body);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
