using AutoMapper;
using CloudJ.Contracts.DTOs.SolutionDtos.Review;
using DataAccessLayer.Models.Solution;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Implementation.AutoMapperProfiles
{
    public class ReviewProfile : Profile
    {
        public ReviewProfile()
        {
            CreateMap<ReviewDto, Review>().ReverseMap();
            CreateMap<NewReviewDto, Review>()
                .ForMember(dest => dest.PostedTime, options => options.MapFrom(x => DateTime.Now))
                .ReverseMap();
        }
    }
}
