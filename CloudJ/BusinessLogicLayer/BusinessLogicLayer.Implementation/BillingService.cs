using AutoMapper;
using BusinessLogicLayer.Abstraction;
using CloudJ.Contracts.DTOs.OrderDtos;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Models.Billing;
using System;
using System.Collections.Generic;
using System.Linq;
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
        public async Task<IReadOnlyCollection<OrderDto>> GetAllAsync()
        {
            var orders = await _orderRepository.GetAllAsync();
            return _mapper.Map<IReadOnlyCollection<OrderDto>>(orders);
        }

        /// <summary>
        /// Получить заказы по фильтру
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<OrderDto>> GetByFilterAsync(OrderFilter filter)
        {
            var orders = await _orderRepository.GetAllAsync();
            if (filter.OrderId != null)
                orders = orders.Where(x => x.Id == filter.OrderId);
            if (filter.CustomerId != null)
                orders = orders.Where(x => x.CustomerId.Equals(filter.CustomerId));
            if (filter.SolutionId != null)
                orders = orders.Where(x => x.SolutionId == filter.SolutionId);
            return _mapper.Map<IReadOnlyCollection<OrderDto>>(orders);
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
