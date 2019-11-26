using AutoMapper;
using CloudJ.Contracts.DTOs.SolutionDtos.Solution;
using DataAccessLayer.Models.Solution;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Implementation.AutoMapperProfiles
{
    public class SolutionProfile : Profile
    {
        public SolutionProfile() {
            CreateMap<SolutionDto, Solution>().ReverseMap();
            CreateMap<NewSolutionDto, Solution>()
                .ForMember(dest => dest.CreatedTime, options => options.MapFrom(source => System.DateTime.Now))
                .ReverseMap();
            CreateMap<RemoveSolutionDto, Solution>().ReverseMap();
            CreateMap<UpdateSolutionDto, Solution>().ReverseMap();
            CreateMap<NewSolutionLinkDto, SolutionLink>().ReverseMap();
            CreateMap<SolutionLinkDto, SolutionLink>().ReverseMap();
        }

    }
}
