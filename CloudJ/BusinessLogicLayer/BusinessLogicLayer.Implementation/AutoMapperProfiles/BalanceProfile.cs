using AutoMapper;
using CloudJ.Contracts.DTOs.OrderDtos;
using DataAccessLayer.Models.Billing;
using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogicLayer.Implementation.AutoMapperProfiles
{
    public class BalanceProfile : Profile
    {
        public BalanceProfile()
        {
            CreateMap<BalanceDto, Balance>().ReverseMap();
            CreateMap<NewBalanceDto, Balance>().ReverseMap();
            CreateMap<UpdateBalanceDto, Balance>().ReverseMap();

        }
    }
}
