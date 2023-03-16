using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;
using SGLNU.BLL.DTO;
using SGLNU.DAL.Enteties;

namespace SGLNU.BLL.Infrastructure.MapperProfiles
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
