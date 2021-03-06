﻿using System;
using MassTransit;

namespace Report
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var bus = Bus.Factory.CreateUsingRabbitMq(cfg =>
            {
                var host = cfg.Host(new Uri("rabbitmq://192.168.139.102"), h =>
                {
                    h.Username("guest");
                    h.Password("guest");
                });

                cfg.ReceiveEndpoint(host, "Report1_Queue",
                    ep =>
                    {
                        ep.Consumer(() => new FileReceivedConsumer());
                    });
            });

            bus.Start();

            Console.WriteLine("Press any key to exit");
            Console.ReadKey();

            bus.Stop();
        }
    }
}