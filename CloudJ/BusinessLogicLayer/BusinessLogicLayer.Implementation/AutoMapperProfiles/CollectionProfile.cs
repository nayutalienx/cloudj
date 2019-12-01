using AutoMapper;
using CloudJ.Contracts.DTOs.CollectionDtos;
using DataAccessLayer.Models.Collection;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Implementation.AutoMapperProfiles
{
    public class CollectionProfile : Profile
    {
        public CollectionProfile()
        {
            CreateMap<CollectionDto, Collection>().ReverseMap();
            CreateMap<NewCollectionDto, Collection>()
                .ReverseMap();
            CreateMap<RemoveCollectionDto, Collection>().ReverseMap();
            CreateMap<UpdateCollectionDto, Collection>().ReverseMap();
            CreateMap<TruncSolutionDto, TruncSolution>().ReverseMap();


        }
    }
}
