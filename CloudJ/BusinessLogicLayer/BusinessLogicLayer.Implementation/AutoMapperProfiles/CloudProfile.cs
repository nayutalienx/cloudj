using AutoMapper;
using CloudJ.Contracts.DTOs.SolutionDtos.Cloud;
using DataAccessLayer.Models.Solution;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Implementation.AutoMapperProfiles
{
    public class CloudProfile : Profile
    {
        public CloudProfile() 
        {
            CreateMap<CloudDto, Cloud>().ReverseMap();
            CreateMap<NewCloudDto, Cloud>().ReverseMap();
        }
    }
}
