using AutoMapper;
using SGLNU.BLL.DTO;
using SGLNU.DAL.Entities;

namespace SGLNU.Web.ViewModels.MapperProfiles
{
    public class NewsProfile : Profile
    {
        public NewsProfile()
        {
            CreateMap<News, NewsDTO>();
            CreateMap<NewsDTO, News>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}
