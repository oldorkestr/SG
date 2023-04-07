using AutoMapper;
using SGLNU.BLL.DTO;
using SGLNU.DAL.Entities;

namespace SGLNU.Web.ViewModels.MapperProfiles
{
    public class CandidateViewModelProfile : Profile
    {
        public CandidateViewModelProfile()
        {
            CreateMap<CandidateViewModel, CandidateDTO>();
            CreateMap<CandidateDTO, CandidateViewModel>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
