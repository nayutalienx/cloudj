using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudJ.Client.Models;
using CloudJ.Client.Clients;
using Microsoft.AspNetCore.Authorization;
using CloudJ.Contracts.DTOs.SolutionDtos.Solution;
using CloudJ.Contracts.DTOs.OrderDtos;

namespace CloudJ.Client.Controllers
{

    public class HomeController : Controller
    {
        private readonly ISolutionApiClient _solutionApiClient;
        private readonly IBillingApiClient _billingApiClient;

        public HomeController(ISolutionApiClient solutionApiClient, IBillingApiClient billingApiClient)
        {
            _solutionApiClient = solutionApiClient;
            _billingApiClient = billingApiClient;
        }

        /// <summary>
        /// Главная страницы
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Index()
        {
            // пример запроса к апи
            var response = await _solutionApiClient.GetAllCategoriesAsync();
            ViewBag.Categories = response.Data;
            return View();
        }

        /// <summary>
        /// Страница категорий
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Category()
        {
            var response = await _solutionApiClient.GetAllCategoriesAsync();
            ViewBag.Categories = response.Data;
            return View();
        }

        /// <summary>
        /// Страница маркетплейса
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> MarketPlace()
        {
            var response = await _solutionApiClient.GetAllCategoriesAsync();
            ViewBag.Categories = response.Data;

            var sols = await _solutionApiClient.GetByFilterAsync(new SolutionFilter { });
            ViewBag.Solutions = sols.Data;

            string userid = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            var bal = await _billingApiClient.GetBalanceByFilterAsync(new BalanceFilter { UserId = userid });
            ViewBag.Balance = bal.Data.FirstOrDefault().BalanceValue;

            return View();
        }

        /// <summary>
        /// Страница поддержки
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Support()
        {
            return View();
        }

        /// <summary>
        /// Редирект на страницу входа IdentityServer
        /// </summary>
        /// <returns></returns>
        [Authorize]
        public async Task<IActionResult> Login()
        {
            string id = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            var response = await _solutionApiClient.GetByFilterAsync(new SolutionFilter { DeveloperId = id });
            ViewBag.PushedSolutions = response.Data;
            string userid = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            var bal = await _billingApiClient.GetBalanceByFilterAsync(new BalanceFilter { UserId = userid });
            ViewBag.Balance = bal.Data.FirstOrDefault().BalanceValue;
            var purchased = await _billingApiClient.GetByFilterAsync(new OrderFilter { CustomerId = id });
            List<SolutionDto> purchasedList = new List<SolutionDto>();
            foreach(OrderDto o in purchased.Data)
            {
                var tempSolution = o.Solution;
                int day = 0;
                if (o.Plan.Time.Equals("День"))
                    day = 1;
                if (o.Plan.Time.Equals("Месяц"))
                    day = 30;
                if (o.Plan.Time.Equals("Год"))
                    day = 360;
                tempSolution.ExpireTime = o.CreatedTime.AddDays(day);
                purchasedList.Add(tempSolution);
            }
            ViewBag.PurchasedSolutions = purchasedList;

            return View();
        }

        /// <summary>
        /// Редирект на выход + выход с площадки
        /// </summary>
        /// <returns></returns>
        public async Task<IActionResult> Logout()
        {
            return SignOut("Cookies", "oidc");
        }

        /// <summary>
        /// Страница коллекций
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("collections")]
        public async Task<IActionResult> Collection()
        {
            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }



        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
