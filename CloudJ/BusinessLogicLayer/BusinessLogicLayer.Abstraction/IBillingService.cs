﻿using CloudJ.Contracts.DTOs.OrderDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstraction
{
    public interface IBillingService
    {
        /// <summary>
        /// Сделать заказ
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<OrderDto> MakeOrderAsync(NewOrderDto dto);
        /// <summary>
        /// Получить все заказы
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyCollection<OrderDto>> GetAllAsync();
        /// <summary>
        /// Получить заказы по фильтру
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<OrderDto>> GetByFilterAsync(OrderFilter filter);

        /// <summary>
        /// Получение балансов по фильтру
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<BalanceDto>> GetBalanceByFilterAsync(BalanceFilter filter);
        /// <summary>
        /// Добавление нового баланса
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BalanceDto> AddBalanceAsync(NewBalanceDto dto);
        /// <summary>
        /// Обновление баланса пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<BalanceDto> UpdateBalanceAsync(UpdateBalanceDto dto);


    }
}


