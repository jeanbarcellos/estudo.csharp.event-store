using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestEventStore.Core.Messages;

namespace TestEventStore.Core.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {
        Task SaveEvent<TEvent>(TEvent theEvent) where TEvent : Event;

        Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId, long start = 0, int count = 500);

        Task<StoredEvent> GetFirstEvent(Guid aggregateId);

        Task<StoredEvent> GetLastEvent(Guid aggregateId);
    }
}
