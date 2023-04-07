using AutoMapper;
using SGLNU.BLL.DTO;

namespace SGLNU.Web.ViewModels.MapperProfiles
{
    public class FacultyViewModelProfile : Profile
    {
        public FacultyViewModelProfile()
        {
            CreateMap<FacultyDTO, FacultyViewModel>();
            CreateMap<FacultyViewModel, FacultyDTO>()
                .ForMember(x => x.Id, c => c.MapFrom(l => l.FacultyId))
                .ForMember(x => x.Name, c => c.MapFrom(l => l.Name))
                .ForMember(x => x.Description, opt => opt.Ignore())
                .ForMember(x => x.Email, opt => opt.Ignore())
                .ForMember(x => x.LogoFilePath, opt => opt.Ignore())
                .ForMember(x => x.UniversityId, opt => opt.Ignore());
        }
    }
}
