using AutoMapper;
using SGLNU.BLL.DTO;
using SGLNU.DAL.Entities;

namespace SGLNU.Web.ViewModels.MapperProfiles
{
    public class CandidateProfile: Profile
    {
        public CandidateProfile()
        {
            CreateMap<Candidate, CandidateDTO>();
                //.ForMember(x => x.Photo, opt => opt.Ignore());
            CreateMap<CandidateDTO, Candidate>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
