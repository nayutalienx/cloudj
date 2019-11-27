using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CloudJ.Client.Models;
using CloudJ.Client.Clients;

namespace CloudJ.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly ISolutionApiClient _solutionApiClient;

        public HomeController(ISolutionApiClient solutionApiClient)
        {
            _solutionApiClient = solutionApiClient;
        }

        public async Task<IActionResult> Index()
        {
            var response = await _solutionApiClient.GetAllCategoriesAsync();
            ViewBag.Categories = response.Data;
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
