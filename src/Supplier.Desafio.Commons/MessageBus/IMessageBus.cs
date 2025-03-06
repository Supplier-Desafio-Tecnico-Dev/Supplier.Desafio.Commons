using EasyNetQ;

namespace Supplier.Desafio.Commons.MessageBus;

public interface IMessageBus : IDisposable
{
    bool IsConnected { get; }
    IAdvancedBus AdvancedBus { get; }

    Task PublishAsync<T>(T message) where T : class;

    void SubscribeAsync<T>(string subscriptionId, Func<T, Task> onMessage) where T : class;
}