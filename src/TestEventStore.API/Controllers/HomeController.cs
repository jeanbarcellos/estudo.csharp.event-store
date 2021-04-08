using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TestEventStore.API.Events;
using TestEventStore.Core.Data.EventSourcing;

namespace TestEventStore.API.Controllers
{
    [ApiController]
    [Route("/events")]
    public class HomeController : ControllerBase
    {
        private readonly IEventSourcingRepository _eventSourcingRepository;
        private Guid _userId;


        public HomeController(IEventSourcingRepository eventSourcingRepository)
        {
            _eventSourcingRepository = eventSourcingRepository;
            _userId = new Guid("28df745b-4127-4e89-8af1-9d6ea2cb260e");
        }

        [HttpGet]
        [Route("")]
        public async Task<IActionResult> ListEventsAsync()
        {
            var events = await _eventSourcingRepository.GetEvents(_userId);

            return Ok(events);
        }

        [HttpGet]
        [Route("first")]
        public async Task<IActionResult> ShowFirstEventAsync()
        {
            var events = await _eventSourcingRepository.GetFirstEvent(_userId);

            return Ok(events);
        }

        [HttpGet]
        [Route("last")]
        public async Task<IActionResult> ShowLastEventAsync()
        {
            var events = await _eventSourcingRepository.GetLastEvent(_userId);

            return Ok(events);
        }

        [HttpPost]
        [Route("insert")]
        public async Task<IActionResult> InsertEventAsync()
        {
            var eEvent = new UserCreatedEvent(_userId, "Jean Barcellos", "teste@teste");

            await _eventSourcingRepository.SaveEvent(eEvent);

            return Ok(eEvent);
        }

    }
}
