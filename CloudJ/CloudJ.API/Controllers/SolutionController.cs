using BusinessLogicLayer.Abstraction;
using CloudJ.Contracts.DTOs.SolutionDtos;
using CloudJ.Contracts.DTOs.SolutionDtos.Category;
using CloudJ.Contracts.DTOs.SolutionDtos.Plan;
using CloudJ.Contracts.DTOs.SolutionDtos.Solution;
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
    public class SolutionController : ApiController
    {
        private readonly ISolutionService _solutionService;
        public SolutionController(
            ISolutionService solutionService)
        {
            _solutionService = solutionService;
        }

        /// <summary>
        /// Получить все решения    
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAllSolutions()
        {
            var sols = await _solutionService.GetAllAsync();
            return ApiResult(sols);
        }

        /// <summary>
        /// Добавить решение
        /// </summary>
        /// <param name="solution"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddSolutionAsync([FromBody] NewSolutionDto solution)
        {
            var sol = await _solutionService.CreateAsync(solution);
            return ApiResult(sol);
        }
        /// <summary>
        /// Удалить решение
        /// </summary>
        /// <param name="solution"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteSolutionAsync([FromBody] RemoveSolutionDto solution)
        {
            await _solutionService.RemoveAsync(solution);
            return ApiResult("Deleted");
        }

        /// <summary>
        /// Добавить категорию
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("categories")]
        public async Task<IActionResult> AddCategoryAsync([FromBody] NewCategoryDto category)
        {
            var cat = await _solutionService.AddCategoryAsync(category);
            return ApiResult(cat);
        }

        /// <summary>
        /// Обновить решение
        /// </summary>
        /// <param name="solution"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateAsync([FromBody] UpdateSolutionDto solution)
        {
            return ApiResult(await _solutionService.UpdateAsync(solution));
        }

        /// <summary>
        /// Получить все категории
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("categories")]
        public async Task<IActionResult> GetAllCategoriesAsync()
        {
            var cats = await _solutionService.GetAllCategoriesAsync();
            return ApiResult(cats);
        }

        /// <summary>
        /// Добавить ссылку на решение
        /// </summary>
        /// <param name="link"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("links")]
        public async Task<IActionResult> AddSolutionLink([FromBody] NewSolutionLinkDto link)
        {
            var result = await _solutionService.AddSolutionLink(link);
            return ApiResult(result);
        }

        /// <summary>
        /// Добавить тарифный план для решения
        /// </summary>
        /// <param name="plan"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("plans")]
        public async Task<IActionResult> AddSolutionPlan([FromBody] NewPlanDto plan)
        {
            var result = await _solutionService.AddPlanAsync(plan);
            return ApiResult(result);
        }
    }
}
