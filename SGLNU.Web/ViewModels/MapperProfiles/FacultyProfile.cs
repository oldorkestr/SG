using AutoMapper;
using SGLNU.BLL.DTO;
using SGLNU.DAL.Entities;

namespace SGLNU.Web.ViewModels.MapperProfiles
{
    public class FacultyProfile : Profile
    {
        public FacultyProfile()
        {
            CreateMap<Faculty, FacultyDTO>();
            CreateMap<FacultyDTO, Faculty>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
