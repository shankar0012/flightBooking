using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlightService.Model
{
    public static class FlightConsumer
    {
        //private readonly AppDbContext _appDbContext;
        //public FlightConsumer(AppDbContext appDbContext)
        //{
        //    _appDbContext = appDbContext;
        //}
        private static IAirlineRepository AirRep;
        public static void InitiliseService(IAirlineRepository airRep)
        {

            AirRep = airRep;
        }
        public static void consumer(string QueueName)
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
                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (sender, e) =>
                    {
                        var boay = e.Body.ToArray();
                        var message = Encoding.UTF8.GetString(boay);
                    // var a= _appDbContext.Booking.
                    // Console.WriteLine(message);
                    var isUpdate = AirRep.CancelTicket(message.ToString());
                    };
                channel.BasicConsume(QueueName, true, consumer);
            }
            catch(Exception ex)
            {

            }
        }
    }
}
