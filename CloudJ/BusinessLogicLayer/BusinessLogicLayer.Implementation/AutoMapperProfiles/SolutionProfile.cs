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
        }

    }
}
