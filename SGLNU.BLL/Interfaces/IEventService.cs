using System.Collections.Generic;
using SGLNU.BLL.DTO;

namespace SGLNU.BLL.Interfaces
{
    public interface IEventService
    {
        public IEnumerable<EventDTO> GetAll();
        public void CreateEvents(EventDTO eventDto);
    }
}