using AutoMapper;
using SGLNU.BLL.DTO;
using SGLNU.DAL.Entities;

namespace SGLNU.Web.ViewModels.MapperProfiles
{
    public class VotingViewModelProfile : Profile
    {
        public VotingViewModelProfile()
        {
            CreateMap<VotingDTO, VotingViewModel>();
                //.ForMember(x => x.Message, opt => opt.Ignore());
            CreateMap<VotingViewModel, VotingDTO>();
        }
    }
}
