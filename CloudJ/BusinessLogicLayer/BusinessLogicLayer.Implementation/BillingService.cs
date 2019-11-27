using AutoMapper;
using BusinessLogicLayer.Abstraction;
using CloudJ.Contracts.DTOs.OrderDtos;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Models.Billing;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Implementation
{
    public class BillingService : IBillingService
    {
        private readonly IOrderRepository _orderRepository;
        private readonly IMapper _mapper;
        public BillingService(IOrderRepository orderRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Получить все заказы
        /// </summary>
        /// <returns></returns>
        public Task<IReadOnlyCollection<OrderDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получить заказы по фильтру
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Task<IReadOnlyCollection<OrderDto>> GetByFilterAsync(OrderFilter filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Добавить заказ
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<OrderDto> MakeOrderAsync(NewOrderDto dto)
        {
            var order = await _orderRepository.AddAsync(_mapper.Map<Order>(dto));
            await _orderRepository.SaveChangesAsync();
            return _mapper.Map<OrderDto>(order);
        }
    }
}
