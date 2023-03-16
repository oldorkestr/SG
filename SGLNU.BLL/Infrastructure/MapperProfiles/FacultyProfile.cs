using AutoMapper;
using SGLNU.BLL.DTO;
using SGLNU.DAL.Enteties;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGLNU.BLL.Infrastructure.MapperProfiles
{
    class FacultyProfile : Profile
    {
        public FacultyProfile()
        {
            CreateMap<Faculty, FacultyDTO>();
            CreateMap<FacultyDTO, Faculty>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
