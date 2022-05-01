using System;
using System.Collections.Generic;
using System.Linq;

namespace EventManagementApiDocker.Services
{
    public record Event(int Id, DateTime Date, string Location, string Description);
    public class EventRepository : IEventRepository
    {
        // In real life use EFCore
        private List<Event> Events { get; } = new();
        public Event Add(Event newEvent)
        {
            Events.Add(newEvent);
            return newEvent;
        }

        public IEnumerable<Event> GetAll()
        {
            return Events;
        }

        public Event GetById(int id) => Events.FirstOrDefault(e => e.Id == id); 

        public void Delete(int id)
        {
            var eventDelete = GetById(id);
            if (eventDelete == null)
            {
                throw new ArgumentException("No event exists with the given id", nameof(id));
            } else
            {
                Events.Remove(eventDelete);
            }
        }
    }
}
