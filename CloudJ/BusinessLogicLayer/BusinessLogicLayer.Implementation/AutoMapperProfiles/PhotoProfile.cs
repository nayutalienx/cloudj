using AutoMapper;
using CloudJ.Contracts.DTOs.SolutionDtos.Photo;
using DataAccessLayer.Models.Solution;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Implementation.AutoMapperProfiles
{
    public class PhotoProfile : Profile
    {
        public PhotoProfile()
        {
            CreateMap<PhotoDto, Photo>().ReverseMap();
            CreateMap<NewPhotoDto, Photo>().ReverseMap();
        }
    }
}
