using CloudJ.Client.Clients;
using CloudJ.Client.Models;
using CloudJ.Contracts.DTOs.SolutionDtos.Cloud;
using CloudJ.Contracts.DTOs.SolutionDtos.Photo;
using CloudJ.Contracts.DTOs.SolutionDtos.Solution;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
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

        /// <summary>
        /// Добавить решение(первая форма с выбором докера или нашей спецификации)
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("add")]
        public async Task<IActionResult> AddSolution()
        {
            return View();
        }

        /// <summary>
        /// Страница добавления по нашей спецификации
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpGet]
        [Route("addPersonalPanel")]
        public async Task<IActionResult> AddPersonalSolution()
        {
            var categories = await _solutionApiClient.GetAllCategoriesAsync();
            ViewBag.Categories = categories.Data;
            return View();
        }

        /// <summary>
        /// Запрос к апи на добавление решения по нашей спецификации
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [Authorize]
        [HttpPost]
        [Route("addPersonalPanel")]
        public async Task<IActionResult> AddPersonalSolution(PersonalSolutionModel model)
        {
            model.UserId = User.Claims.FirstOrDefault(c => c.Type.Contains("nameidentifier")).Value;
            NewSolutionDto dto = new NewSolutionDto 
            { 
                UserId = model.UserId,
                Name = model.Name,
                Description = model.Description,
                CategoryId = model.CategoryId,
                Cloud = new NewCloudDto 
                {
                    Name = model.CloudName,
                    Url = model.CloudUrl
                }
            };
            List<NewPhotoDto> photoList = new List<NewPhotoDto>();

            var ph = model.Logo;
            byte[] p1 = null;
            using (var fs1 = ph.OpenReadStream())
            using (var ms1 = new MemoryStream())
            {
                fs1.CopyTo(ms1);
                p1 = ms1.ToArray();
            }

            photoList.Add(new NewPhotoDto { Data = p1, Type = "Logo" });

            foreach (var photo in model.Photos)
            {
                p1 = null;
                using (var fs1 = photo.OpenReadStream())
                using (var ms1 = new MemoryStream())
                {
                    fs1.CopyTo(ms1);
                    p1 = ms1.ToArray();
                }

                photoList.Add(new NewPhotoDto { Data = p1, Type="Description" });
            }
            dto.Photos = photoList.ToArray();

            var response = await _solutionApiClient.AddSolutionAsync(dto);

            long id = response.Data.Id;

            return Redirect($"~/Product/{id}");
        }

        /// <summary>
        /// Страница добавления с помощью докер контейнера
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("addDocker")]
        public async Task<IActionResult> AddDocker()
        {
            return View();
        }
    }
}
