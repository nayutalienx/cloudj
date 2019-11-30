using CloudJ.Client.Clients;
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
        
        public StoreController(IBillingApiClient billingApiClient)
        {
            _billingApiClient = billingApiClient;
        }

        /// <summary>
        /// Пополнить баланс
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> FillBalance()
        {
            return View();
        }
    }
}
