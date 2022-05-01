using System.Collections.Generic;

namespace EventManagementApiDocker.Services
{
    public interface IEventRepository
    {
        Event Add(Event newEvent);
        IEnumerable<Event> GetAll();

        Event GetById(int id);

        void Delete(int id);
    }
}
