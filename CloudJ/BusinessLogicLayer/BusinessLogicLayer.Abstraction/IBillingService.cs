using CloudJ.Contracts.DTOs.OrderDtos;
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
        Task<OrderDto> MakeOrderAsync(OrderDto dto);
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


    }
}


