using AutoMapper;
using SGLNU.BLL.DTO;
using SGLNU.DAL.Entities;

namespace SGLNU.Web.ViewModels.MapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, UserDTO>();
            CreateMap<UserDTO, AppUser>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}