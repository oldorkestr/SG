using System;
using AutoMapper;
using SGLNU.BLL.DTO;
using SGLNU.DAL.Entities;

namespace SGLNU.Web.ViewModels.MapperProfiles
{
    public class CandidateViewModelProfile : Profile
    {
        public CandidateViewModelProfile()
        {
            CreateMap<CandidateViewModel, CandidateDTO>()
                .ForMember(x => x.Photo, opt => opt.Ignore());
                
            CreateMap<CandidateDTO, CandidateViewModel>()
                .ForMember(x => x.Photo, c => c.MapFrom(l => l.Photo != null ? Convert.ToBase64String(l.Photo) : null)); ;
            //.ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
