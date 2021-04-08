using EventStore.ClientAPI;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestEventStore.Core.Data.EventSourcing;
using TestEventStore.Core.Messages;

namespace TestEventStore.EventSourcing
{
    public class EventSourcingRepository : IEventSourcingRepository
    {
        private readonly IEventStoreService _eventStoreService;

        public EventSourcingRepository(IEventStoreService eventStoreService)
        {
            _eventStoreService = eventStoreService;
        }

        public async Task SaveEvent<TEvent>(TEvent theEvent) where TEvent : Event
        {
            await _eventStoreService.GetConnection().AppendToStreamAsync(
                    theEvent.AggregateId.ToString(),
                    ExpectedVersion.Any,
                    FormatEvent(theEvent)
                );
        }

        public async Task<IEnumerable<StoredEvent>> GetEvents(Guid aggregateId, long start = 0, int count = 500)
        {
            var events = await _eventStoreService.GetConnection()
                .ReadStreamEventsForwardAsync(aggregateId.ToString(), start, count, false);

            var listEvents = new List<StoredEvent>();

            foreach (var resolvedEvent in events.Events)
            {
                var data = Encoding.UTF8.GetString(resolvedEvent.Event.Data);
                var json = JsonConvert.DeserializeObject<BaseEvent>(data);

                var theEvent = new StoredEvent(
                    resolvedEvent.Event.EventId,
                    resolvedEvent.Event.EventType,
                    json.Timestamp,
                    data
                );

                listEvents.Add(theEvent);
            }

            return listEvents;
        }

        public async Task<StoredEvent> GetFirstEvent(Guid aggregateId)
        {
            var events = await GetEvents(aggregateId, 0, 1);

            return events.First();
        }

        public async Task<StoredEvent> GetLastEvent(Guid aggregateId)
        {
            var events = await GetEvents(aggregateId, 0, 1000);

            return events.Last();
        }

        private static IEnumerable<EventData> FormatEvent<TEvent>(TEvent theEvent) where TEvent : Event
        {
            yield return new EventData(
                Guid.NewGuid(),
                theEvent.MessageType,
                true,
                Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(theEvent)),
                null
            );
        }
    }

}
