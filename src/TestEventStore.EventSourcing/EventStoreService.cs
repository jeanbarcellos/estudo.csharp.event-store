﻿using EventStore.ClientAPI;
using Microsoft.Extensions.Configuration;
using System;

namespace TestEventStore.EventSourcing
{
    public class EventStoreService : IEventStoreService
    {
        private readonly IEventStoreConnection _connection;

        public EventStoreService(IConfiguration configuration)
        {
            _connection = EventStoreConnection.Create(
                new Uri(configuration.GetConnectionString("EventStoreConnection"))
            );
            _connection.ConnectAsync();
        }

        public IEventStoreConnection GetConnection()
        {
            return _connection;
        }
    }
}