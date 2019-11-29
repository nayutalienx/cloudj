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
        private readonly IBalanceRepository _balanceRepository;
        private readonly IMapper _mapper;
        public BillingService(IOrderRepository orderRepository, IBalanceRepository balanceRepository,
            IMapper mapper)
        {
            _orderRepository = orderRepository;
            _balanceRepository = balanceRepository;
            _mapper = mapper;
        }

        /// <summary>
        /// Добавить новый баланс
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<BalanceDto> AddBalanceAsync(NewBalanceDto dto)
        {
            var result = await _balanceRepository.AddAsync(_mapper.Map<Balance>(dto));
            await _balanceRepository.SaveChangesAsync();
            return _mapper.Map<BalanceDto>(result);
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
        /// Получить балансы по фильтру
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<BalanceDto>> GetBalanceByFilterAsync(BalanceFilter filter)
        {
            var balances = await _balanceRepository.GetAllAsync();

            if (filter.Id != null)
                balances = balances.Where(b => b.Id == filter.Id);
            if (filter.UserId != null)
                balances = balances.Where(b => b.UserId.Equals(filter.UserId));

            return _mapper.Map<IReadOnlyCollection<BalanceDto>>(balances);

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

        /// <summary>
        /// Обновить баланс 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<BalanceDto> UpdateBalanceAsync(UpdateBalanceDto dto)
        {
            var balances = await _balanceRepository.GetAllAsync();
            var bal = balances.Where(b => b.UserId.Equals(dto.UserId)).FirstOrDefault();
            bal.BalanceValue = dto.BalanceValue;
            await _balanceRepository.SaveChangesAsync();
            return _mapper.Map<BalanceDto>(bal);
        }
    }
}
