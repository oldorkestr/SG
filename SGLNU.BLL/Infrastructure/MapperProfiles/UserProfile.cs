using System;
using System.Collections.Generic;
using System.Text;
using SGLNU.BLL.DTO;
using SGLNU.DAL.Enteties;
using AutoMapper;

namespace SGLNU.BLL.Infrastructure.MapperProfiles
{
    class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<AppUser, UserDTO>();
            CreateMap<UserDTO, AppUser>()
                .ForMember(x => x.Id, opt => opt.Ignore());
        }
    }
}