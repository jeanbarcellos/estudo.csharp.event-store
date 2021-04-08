using EventStore.ClientAPI;

namespace TestEventStore.EventSourcing
{
    public interface IEventStoreService
    {
        IEventStoreConnection GetConnection();
    }
}
