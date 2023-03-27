using AutoMapper;
using SGLNU.BLL.DTO;
using SGLNU.DAL.Entities;

namespace SGLNU.BLL.Infrastructure.MapperProfiles
{
    public class VotingProfile : Profile
    {
        public VotingProfile()
        {
            CreateMap<Voting, VotingDTO>();
            CreateMap<VotingDTO, Voting>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
