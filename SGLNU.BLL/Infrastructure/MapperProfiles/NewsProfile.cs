using AutoMapper;
using SGLNU.BLL.DTO;
using SGLNU.DAL.Enteties;
using System;
using System.Collections.Generic;
using System.Text;

namespace SGLNU.BLL.Infrastructure.MapperProfiles
{
    class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<News, NewsDTO>();
            CreateMap<NewsDTO, News>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
