using CloudJ.Client.Clients;
using CloudJ.Client.Models;
using CloudJ.Contracts.DTOs.OrderDtos;
using CloudJ.Contracts.DTOs.SolutionDtos.Solution;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CloudJ.Client.Controllers
{
    [Controller]
    [Route("[controller]")]
    public class StoreController : Controller
    {
        private readonly IBillingApiClient _billingApiClient;
        private readonly ISolutionApiClient _solutionApiClient;
        
        public StoreController(IBillingApiClient billingApiClient, ISolutionApiClient solutionApiClient)
        {
            _billingApiClient = billingApiClient;
            _solutionApiClient = solutionApiClient;
        }

        /// <summary>
        /// Пополнить баланс(форма ввода карты)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> FillBalance(double balance)
        {
            return View(new BalanceModel { BalanceValue = balance});
        }

        /// <summary>
        /// Запрос к апи на пополнение баланса
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> FillBalance(BalanceModel model)
        {
            string userid = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            var response = await _billingApiClient.GetBalanceByFilterAsync(new BalanceFilter { UserId = userid});

            if(response.Data?.FirstOrDefault() == null)
            {
                var resp = await _billingApiClient.AddBalanceAsync(new NewBalanceDto { UserId = userid, BalanceValue = model.BalanceValue });
            }
            else
            {
                var resp = await _billingApiClient.UpdateBalanceAsync(new UpdateBalanceDto { UserId = userid, BalanceValue = model.BalanceValue + response.Data.FirstOrDefault().BalanceValue});
            }
            
            return Redirect("~/");
        }

        /// <summary>
        /// Запрос к апи на покупку решения
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("makeOrder")]
        public async Task<IActionResult> MakeOrder(NewOrderDto dto)
        {
            var solutionResponse = await _solutionApiClient.GetByFilterAsync(new SolutionFilter { SolutionId = dto.SolutionId });
            var sol = solutionResponse.Data.FirstOrDefault();
            var plan = sol.Plans.Where(p => p.Id == dto.PlanId).FirstOrDefault();
            dto.CustomerId = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            var balanceResponse = await _billingApiClient.GetBalanceByFilterAsync(new BalanceFilter { UserId = dto.CustomerId });
            var balance = balanceResponse.Data.FirstOrDefault();
            if (balance.BalanceValue < plan.Price)
                return Redirect("~/Home/Login");
            var response = await _billingApiClient.AddOrderAsync(dto);
            if (response.HasErrors)
                return View("Error", new ErrorViewModel { RequestId = "Ошибка при обработке заказа" });

            
            
            
            
                await _billingApiClient.UpdateBalanceAsync(new UpdateBalanceDto { UserId = dto.CustomerId, BalanceValue = balance.BalanceValue - plan.Price });
            
            return Redirect("~/Home/Login");
        }

        /// <summary>
        /// Пополнение баланса (форма ввода кол-ва денег на пополнение)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("up")]
        public async Task<IActionResult> UpBalance()
        {
            return View();
        }


    }
}
