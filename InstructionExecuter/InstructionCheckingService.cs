using Infrastructure.Repositories;
using Application.Repositories;
using Domain;
using Domain.Enums;
using RabbitMQ.Client;
using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
public class InstructionCheckingService
{
    IServiceProvider _serviceProvider;
    public InstructionCheckingService(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
    }
    public async Task Execute()
    {
        var repo = _serviceProvider.GetService<IInstructionRepo>();

        if (repo != null)
        {
            DateTime today = DateTime.Today;
            int dayOfMonth = today.Day;

            List<Instruction> instructions = await repo.GetAllAsync();
            if (instructions != null)
            {
                foreach (var instruction in instructions)
                {
                    if (instruction.DayOfMonth == dayOfMonth)
                    {
                        ExecuteInstruction(instruction);
                    }
                }
            }
        }
        else
        {
            System.Console.WriteLine("InstructionRepo null");
        }
    }
    public async Task ExecuteInstruction(Instruction instruction)
    {
        System.Console.WriteLine($"Instruction(id:{instruction.Id}) is executed!");
        try
        {
            System.Console.WriteLine("Getting notifications");
            var repo = _serviceProvider.GetService<INotificationRepo>();


            var notifications = await repo.GetByInstructionIdAsync(instruction.Id);
            if (notifications != null)
            {
                foreach (SendNotificaitonDto notification in notifications)
                {
                    System.Console.WriteLine($"Bildirim Id:{notification.Id} - Tür:{notification.ChannelType}");
                    InformUser(notification);
                }
            }
            else
                System.Console.WriteLine("notifications null");
        }
        catch (Exception e)
        {
            System.Console.WriteLine("Error on ExecuteInstruction:", e.Message);
            throw;
        }


    }

    public void InformUser(SendNotificaitonDto notification)
    {
        System.Console.WriteLine("Informing user...");
        var message = "";

        message = JsonSerializer.Serialize(notification);


        System.Console.WriteLine("HandleSendToWhichQueue : " + notification.ChannelType);
        if (notification.ChannelType == ChannelType.Email)
        {
            SendQueue(ChannelType.Email, message);
        }
        else if (notification.ChannelType == ChannelType.Sms)
        {
            System.Console.WriteLine("Sending Sms");
            SendQueue(ChannelType.Sms, message);
        }
        else if (notification.ChannelType == ChannelType.PushNotification)
        {
            SendQueue(ChannelType.PushNotification, message);
        }
    }

    void SendQueue(ChannelType channelType, string message)
    {
        var queueName = channelType == ChannelType.Sms ? "sms_queue" : (channelType == ChannelType.Email ? "email_queue" : "push_queue");

        try
        {
            var factory = new ConnectionFactory { HostName = "localhost" };
            using var connection = factory.CreateConnection();
            using var channel = connection.CreateModel();


            channel.QueueDeclare(queue: queueName,
                                 durable: false,
                                 exclusive: false,
                                 autoDelete: false,
                                 arguments: null);

            var body = Encoding.UTF8.GetBytes(message);

            channel.BasicPublish(exchange: string.Empty,
                                 routingKey: queueName,
                                 basicProperties: null,
                                 body: body);

            System.Console.WriteLine(" [x] Sent {0}", message);
        }
        catch (System.Exception)
        {
            System.Console.WriteLine("Error on  Sending queue ");
        }

    }

}