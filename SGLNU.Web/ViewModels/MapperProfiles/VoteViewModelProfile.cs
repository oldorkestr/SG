using AutoMapper;
using SGLNU.BLL.DTO;

namespace SGLNU.Web.ViewModels.MapperProfiles
{
    public class VoteViewModelProfile : Profile
    {
        public VoteViewModelProfile()
        {
            CreateMap<VoteDTO, VoteViewModel>();
            CreateMap<VoteViewModel, VoteDTO>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
