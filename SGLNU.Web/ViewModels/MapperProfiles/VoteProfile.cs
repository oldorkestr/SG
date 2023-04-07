using AutoMapper;
using SGLNU.BLL.DTO;
using SGLNU.DAL.Entities;

namespace SGLNU.Web.ViewModels.MapperProfiles
{
    public class VoteProfile : Profile
    {
        public VoteProfile()
        {
            CreateMap<Vote, VoteDTO>();
            CreateMap<VoteDTO, Vote>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
