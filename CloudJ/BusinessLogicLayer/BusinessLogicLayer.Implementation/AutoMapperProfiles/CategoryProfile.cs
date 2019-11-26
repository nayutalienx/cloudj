using AutoMapper;
using CloudJ.Contracts.DTOs.SolutionDtos;
using CloudJ.Contracts.DTOs.SolutionDtos.Category;
using DataAccessLayer.Models.Solution;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Implementation.AutoMapperProfiles
{
    public class CategoryProfile : Profile
    {
        public CategoryProfile() {
            CreateMap<Category, CategoryDto>().ReverseMap();
            CreateMap<NewCategoryDto, Category>();
        }
    }
}
