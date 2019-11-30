using AutoMapper;
using CloudJ.Contracts.DTOs.OrderDtos;
using DataAccessLayer.Models.Billing;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Implementation.AutoMapperProfiles
{
    public class OrderProfile : Profile
    {
        public OrderProfile()
        {
            CreateMap<OrderDto, Order>().ReverseMap();
            CreateMap<NewOrderDto, Order>()
                 .ForMember(dest => dest.CreatedTime, options => options.MapFrom(source => System.DateTime.Now))
                .ReverseMap();
        }
    }
}
