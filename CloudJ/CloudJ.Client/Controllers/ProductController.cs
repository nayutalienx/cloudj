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
    public class ProductController : Controller
    {
        private readonly ISolutionApiClient _solutionApiClient;
        public ProductController(ISolutionApiClient solutionApiClient)
        {
            _solutionApiClient = solutionApiClient;
        }

        /// <summary>
        /// Страница конкретного решения
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:long}")]
        public async Task<IActionResult> GetProductById(long id)
        {
            return View();
        }

        /// <summary>
        /// Добавить ссылку к решению
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("{id:long}/links")]
        public async Task<IActionResult> AddLink(long id)
        {
            return View();
        }
    }
}
