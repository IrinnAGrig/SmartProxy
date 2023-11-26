using Common.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using EventAPI.Repositories;
using EventAPI.Services;

namespace EventAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventController : ControllerBase
    {
        private readonly IMongoRepository<Event> _eventRepository;
        private readonly ISyncService<Event> _eventSyncService;
        public EventController(IMongoRepository<Event> eventRepository, ISyncService<Event> eventSyncService)
        {
               _eventRepository = eventRepository;
               _eventSyncService = eventSyncService;
        }
        [HttpGet]
        public List<Event> GetAllEvents()
        {
            var records = _eventRepository.GetAllRecords();

            return records;
        }

        [HttpGet("{id}")]
        public Event GetEventById(Guid id)
        {
            var result = _eventRepository.GetRecordById(id);
            
            return result;
        }

        [HttpPost]

        public IActionResult Create(Event event1)
        {
            event1.LastChangedAt = DateTime.UtcNow;

            var result = _eventRepository.InsertRecord(event1);

               _eventSyncService.Upsert(event1);

            return Ok(result);
        }

        [HttpPut]
        public IActionResult Upsert(Event event1)
        {
            if (event1.Id != Guid.Empty)
            {
                return BadRequest("Empty Id");
            }

            event1.LastChangedAt = DateTime.UtcNow;
               _eventRepository.UpsertRecord(event1);

               _eventSyncService.Upsert(event1);

            return Ok(event1);
        }

        [HttpPut("sync")]
        public IActionResult UpsertSync(Event event1)
        {
            var exisitingEvent = _eventRepository.GetRecordById(event1.Id);

            if (exisitingEvent == null || event1.LastChangedAt > exisitingEvent.LastChangedAt)
            {
                    _eventRepository.UpsertRecord(event1);
            }

            return Ok();
        }


        [HttpDelete("sync")]
        public IActionResult DeleteSync(Event event1)
        {
            var exisitingEvent = _eventRepository.GetRecordById(event1.Id);

            if (exisitingEvent != null || event1.LastChangedAt > exisitingEvent.LastChangedAt)
            {
                    _eventRepository.DeleteRecord(event1.Id);
            }
            return Ok();
        }


        [HttpDelete("{id}")]

        public IActionResult Delete(Guid id)
        {
            var event1 = _eventRepository.GetRecordById(id);

            if (event1 == null)
            {
                return BadRequest("Event does not exist");
            }

               _eventRepository.DeleteRecord(id);

               event1.LastChangedAt = DateTime.UtcNow;

               _eventSyncService.Delete(event1);

            return Ok("Deleted" + id);
        }


    }
}
