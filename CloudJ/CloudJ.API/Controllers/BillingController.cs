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
    }
}
