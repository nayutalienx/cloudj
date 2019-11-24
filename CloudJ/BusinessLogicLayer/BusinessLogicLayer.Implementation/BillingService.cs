using BusinessLogicLayer.Abstraction;
using CloudJ.Contracts.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Implementation
{
    public class BillingService : IBillingService
    {
        public Task<IReadOnlyCollection<OrderDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<OrderDto>> GetByFilterAsync(OrderFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task<OrderDto> MakeOrderAsync(OrderDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
