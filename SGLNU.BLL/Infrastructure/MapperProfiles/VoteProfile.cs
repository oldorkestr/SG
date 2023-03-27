using AutoMapper;
using SGLNU.BLL.DTO;
using SGLNU.DAL.Entities;

namespace SGLNU.BLL.Infrastructure.MapperProfiles
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
