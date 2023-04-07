using AutoMapper;
using SGLNU.BLL.DTO;
using SGLNU.DAL.Entities;

namespace SGLNU.Web.ViewModels.MapperProfiles
{
    public class VotingViewModelProfile : Profile
    {
        public VotingViewModelProfile()
        {
            CreateMap<VotingDTO, VotingViewModel>()
                .ForMember(x => x.FacultyName, f => f.MapFrom(v => v.Faculty.Name));
                
            CreateMap<VotingViewModel, VotingDTO>();
                //.ForMember(x => x.Faculty.Name, f => f.MapFrom(v=>v.FacultyName));
        }
    }
}
