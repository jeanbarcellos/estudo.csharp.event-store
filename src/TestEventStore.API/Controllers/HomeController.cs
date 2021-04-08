using Microsoft.AspNetCore.Mvc;
using System;
using System.Text;
using System.Threading.Tasks;
using EventStore.ClientAPI;

namespace TestEventStore.API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HomeController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> GetAsync()
        {
            var conn = EventStoreConnection.Create(new Uri("tcp://admin:changeit@localhost:1113"));
            await conn.ConnectAsync();

            var data = Encoding.UTF8.GetBytes("{\"a\":\"2\"}");
            var metadata = Encoding.UTF8.GetBytes("{}");
            var evt = new EventData(Guid.NewGuid(), "testEvent", true, data, metadata);

            await conn.AppendToStreamAsync("test-stream", ExpectedVersion.Any, evt);

            var streamEvents = await conn.ReadStreamEventsForwardAsync("test-stream", 0, 1, false);
            var returnedEvent = streamEvents.Events[0].Event;

            return Ok($"Read event with data: {Encoding.UTF8.GetString(returnedEvent.Data)}, metadata: {Encoding.UTF8.GetString(returnedEvent.Metadata)}");
        }
    }
}
