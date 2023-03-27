using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using SGLNU.BLL.DTO;
using SGLNU.DAL.Entities;

namespace SGLNU.BLL.Infrastructure.MapperProfiles
{
    public class CandidateProfile: Profile
    {
        public CandidateProfile()
        {
            CreateMap<Candidate, CandidateDTO>();
            CreateMap<CandidateDTO, Candidate>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
