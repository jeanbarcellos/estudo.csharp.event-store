using NerdStore.Core.Data.EventSourcing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestEventStore.Core.Messages;

namespace TestEventStore.Core.Data.EventSourcing
{
    public interface IEventSourcingRepository
    {
        public interface IEventSourcingRepository
        {
            Task SaveEvent<TEvent>(TEvent evento) where TEvent : Event;
            Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId);
        }
    }
}
