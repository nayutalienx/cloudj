using AutoMapper;
using CloudJ.Contracts.DTOs.SolutionDtos.Cloud;
using DataAccessLayer.Models.Solution;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Implementation.AutoMapperProfiles
{
    public class ContainerProfile : Profile
    {
        public ContainerProfile()
        {
            CreateMap<DockerImageDto, DockerImage>().ReverseMap();
            CreateMap<NewDockerImageDto, DockerImage>().ReverseMap();
        }
    }
}
