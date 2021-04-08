using System;

namespace TestEventStore.EventSourcing
{
    internal class BaseEvent
    {
        public DateTime Timestamp { get; set; }
    }
}
