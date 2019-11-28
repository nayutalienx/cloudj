using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;

namespace IdentityServer.Objects.AutoMapperProfiles
{
    public class RoleProfile : Profile
    {
        public RoleProfile() {
            CreateMap<RoleViewModel, IdentityRole>().ReverseMap();
        }
    }
}
