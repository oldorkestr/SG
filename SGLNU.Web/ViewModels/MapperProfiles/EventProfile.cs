using AutoMapper;
using SGLNU.BLL.DTO;
using SGLNU.DAL.Entities;

namespace SGLNU.Web.ViewModels.MapperProfiles
{
    public class EventProfile : Profile
    {
        public EventProfile()
        {
            CreateMap<Event, EventDTO>();
            CreateMap<EventDTO, Event>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
