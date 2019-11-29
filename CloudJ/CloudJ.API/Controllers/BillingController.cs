using BusinessLogicLayer.Abstraction;
using CloudJ.Contracts.DTOs.OrderDtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudJ.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BillingController : ApiController
    {
        private readonly IBillingService _billingService;
        public BillingController(IBillingService billingService)
        {
            _billingService = billingService;
        }

        /// <summary>
        /// Сделать заказ
        /// </summary>
        /// <param name="order"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> MakeOrderAsync([FromBody] NewOrderDto order)
        {
            var result = await _billingService.MakeOrderAsync(order);
            return ApiResult(result);
        }

        /// <summary>
        /// Получить все заказы
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllOrdersAsync()
        {
            var result = await _billingService.GetAllAsync();
            return ApiResult(result);
        }

        /// <summary>
        /// Получить заказы по фильтру
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("filter")]
        public async Task<IActionResult> GetOrdersByFilterAsync([FromBody] OrderFilter filter)
        {
            var result = await _billingService.GetByFilterAsync(filter);
            return ApiResult(result);
        }

        /// <summary>
        /// Добавить баланс пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("balance")]
        public async Task<IActionResult> AddBalanceAsync([FromBody] NewBalanceDto dto)
        {
            var result = await _billingService.AddBalanceAsync(dto);
            return ApiResult(result);
        }

        /// <summary>
        /// Обновить баланс пользователя
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("balance")]
        public async Task<IActionResult> UpdateBalanceAsync([FromBody] UpdateBalanceDto dto)
        {
            var result = await _billingService.UpdateBalanceAsync(dto);
            return ApiResult(result);
        }

        /// <summary>
        /// Получить балансы по фильтру
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("balance")]
        public async Task<IActionResult> GetBalancesByFilterAsync([FromQuery] BalanceFilter filter)
        {
            var result = await _billingService.GetBalanceByFilterAsync(filter);
            return ApiResult(result);
        }
    }
}
