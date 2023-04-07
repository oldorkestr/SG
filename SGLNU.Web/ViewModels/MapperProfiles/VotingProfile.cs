using AutoMapper;
using SGLNU.BLL.DTO;
using SGLNU.DAL.Entities;

namespace SGLNU.Web.ViewModels.MapperProfiles
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
