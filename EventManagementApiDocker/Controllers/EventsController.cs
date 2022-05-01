using EventManagementApiDocker.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;

namespace EventManagementApiDocker.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EventsController : Controller
    {
        private readonly IEventRepository repository;
        public EventsController(IEventRepository repository)
        {
            this.repository = repository;
        }

        //IEnumerable<Event> GetAll();

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Event>), StatusCodes.Status200OK)]
        public IActionResult GetAll()
        {
            return Ok(repository.GetAll());
        }

        //public IActionResult GetAll() => Ok(repository.GetAll());

        //Event GetById(int id);
        [HttpGet("{id}", Name = nameof(GetById))]
        //[Route("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(Event), StatusCodes.Status200OK)]
        public IActionResult GetById(int id)
        {
            var existingEvent = repository.GetById(id);
            if (existingEvent == null) return NotFound();
            return Ok(existingEvent);

        }

        //Event Add(Event newEvent);
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(Event), StatusCodes.Status200OK)]
        public IActionResult Add([FromBody] Event newEvent)
        {
            if (newEvent.Id < 1)
            {
                return BadRequest("Invalid Id");
            }
            repository.Add(newEvent);
            return CreatedAtAction(nameof(GetById), new { id = newEvent.Id }, newEvent);


        }



        //void Delete(int id);
        [HttpDelete]
        [Route("{eventToDeleteId}")]
        public IActionResult Delete(int eventToDeleteId)
        {
            try
            {
                repository.Delete(eventToDeleteId);
            }
            catch (ArgumentException)
            {
                return NotFound();
            }

            return NoContent();
        }
    }
}
