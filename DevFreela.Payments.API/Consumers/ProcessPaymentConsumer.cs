using System.Text;
using System.Text.Json;
using DevFreela.Payments.API.Models;
using DevFreela.Payments.API.Services.Interfaces;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace DevFreela.Payments.API.Consumers;

public class ProcessPaymentConsumer : BackgroundService
{
    private readonly IConnection _connection;
    private readonly IModel _channel;
    private IServiceProvider _serviceProvider;

    private const string QUEUE = "Payments";
    private const string PAYMENT_APPROVED_QUEUE = "PaymentsApproved";

    public ProcessPaymentConsumer(IServiceProvider serviceProvider)
    {
        _serviceProvider = serviceProvider;
        var factory = new ConnectionFactory
        {
            HostName = "localhost"
        };
        _connection = factory.CreateConnection();
        _channel = _connection.CreateModel();

        _channel.QueueDeclare(
            queue: QUEUE,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        _channel.QueueDeclare(
            queue: PAYMENT_APPROVED_QUEUE,
            durable: false,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );
    }

    protected override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var consumer = new EventingBasicConsumer(_channel);
        consumer.Received += (sender, eventArgs) =>
        {
            var byteArray = eventArgs.Body.ToArray();
            var paymentInfoJson = Encoding.UTF8.GetString(byteArray);
            var paymentInfo = JsonSerializer.Deserialize<PaymentInputModel>(paymentInfoJson);

            ProcessPayment(paymentInfo);

            var paymentApproved = new PaymentApprovedIntegrationEvent(paymentInfo.idProject);
            var paymentApprovedJson = JsonSerializer.Serialize(paymentApproved);
            var paymentApprovedBytes = Encoding.UTF8.GetBytes(paymentApprovedJson);

            _channel.BasicPublish(
                exchange: "",
                routingKey: PAYMENT_APPROVED_QUEUE,
                basicProperties: null,
                body: paymentApprovedBytes
            );

            _channel.BasicAck(eventArgs.DeliveryTag, false);
        };

        _channel.BasicConsume(QUEUE, false, consumer);

        return Task.CompletedTask;
    }

    public void ProcessPayment(PaymentInputModel paymentInputModel)
    {
        using (var scope = _serviceProvider.CreateScope())
        {
            var paymentService = scope.ServiceProvider.GetRequiredService<IPaymentService>();
            paymentService.Process(paymentInputModel);
        }
    }
}