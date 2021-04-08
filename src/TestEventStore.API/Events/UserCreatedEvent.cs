using System;
using TestEventStore.Core.Messages;

namespace TestEventStore.API.Events
{
    public class UserCreatedEvent : Event
    {
        public Guid UserId { get; private set; }
        public string Name { get; private set; }
        public string Email { get; private set; }

        public UserCreatedEvent(Guid userId, string name, string email)
        {
            AggregateId = userId;
            UserId = userId;
            Name = name;
            Email = email;
        }
    }
}
