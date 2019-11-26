using AutoMapper;
using CloudJ.Contracts.DTOs.SolutionDtos.Plan;
using DataAccessLayer.Models.Solution;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Implementation.AutoMapperProfiles
{
    public class PlanProfile : Profile
    {
        public PlanProfile()
        {
            CreateMap<PlanDto, Plan>().ReverseMap();
            CreateMap<NewPlanDto, Plan>().ReverseMap();
        }
    }
}
