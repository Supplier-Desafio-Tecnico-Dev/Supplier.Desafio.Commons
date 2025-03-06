using EasyNetQ;
using Polly;
using RabbitMQ.Client.Exceptions;

namespace Supplier.Desafio.Commons.MessageBus;

public class MessageBus : IMessageBus
{
    private IBus _bus;
    private IAdvancedBus _advancedBus;
    private readonly string _connectionString;

    public MessageBus(string connectionString)
    {
        _connectionString = connectionString;
        TryConnect();
    }
    
    public bool IsConnected => _bus?.Advanced.IsConnected ?? false;
    public IAdvancedBus AdvancedBus => _bus?.Advanced;
    
    public async Task PublishAsync<T>(T message) where T : class
    {
        TryConnect();
        await _bus.PubSub.PublishAsync(message);
    }
    
    public void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class
    {
        TryConnect();
        _bus.PubSub.SubscribeAsync(subscriptionId, onMessage);
    }
    
    private void TryConnect()
    {
        if (IsConnected) return;

        var policy = Policy.Handle<EasyNetQException>()
            .Or<BrokerUnreachableException>()
            .WaitAndRetry(3, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                (exception, timeSpan, retryCount, context) =>
                {
                    Console.WriteLine($"Tentativa {retryCount} falhou: {exception.Message}");
                });

        policy.Execute(() =>
        {
            Console.WriteLine("Tentando conectar ao RabbitMQ...");
            _bus = RabbitHutch.CreateBus(_connectionString);
            _advancedBus = _bus.Advanced;
            _advancedBus.Disconnected += OnDisconnect;

            Console.WriteLine("Conectado ao RabbitMQ!");
        });
    }

    
    private void OnDisconnect(object s, EventArgs e)
    {
        var policy = Policy.Handle<EasyNetQException>()
            .Or<BrokerUnreachableException>()
            .RetryForever();

        policy.Execute(TryConnect);
    }
    
    public void Dispose()
    {
        _bus.Dispose();
    }
}