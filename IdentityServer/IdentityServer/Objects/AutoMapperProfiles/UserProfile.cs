using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Objects.AutoMapperProfiles
{
    public class UserProfile : Profile
    {
        public UserProfile()
        {
            CreateMap<IdentityUser, UserViewModel>().ReverseMap();
            CreateMap<IdentityUser, RegisterViewModel>().ReverseMap();
            CreateMap<IdentityUser, EditUserViewModel>().ReverseMap();
        }
    }
}
