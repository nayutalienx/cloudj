using BusinessLogicLayer.Abstraction;
using CloudJ.Contracts.DTOs.SolutionDtos;
using CloudJ.Contracts.DTOs.SolutionDtos.Category;
using CloudJ.Contracts.DTOs.SolutionDtos.Plan;
using CloudJ.Contracts.DTOs.SolutionDtos.Review;
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
        public async Task<IActionResult> GetAllSolutionsAsync()
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
        /// Удалить категорию
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpDelete]
        [Route("categories")]
        public async Task<IActionResult> DeleteCategoryAsync([FromBody] RemoveCategoryDto category)
        {
            await _solutionService.RemoveCategoryAsync(category);
            return ApiResult("Deleted.");
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
        /// Обновить категорию
        /// </summary>
        /// <param name="category"></param>
        /// <returns></returns>
        [HttpPut]
        [Route("categories")]
        public async Task<IActionResult> UpdateCategoryAsync([FromBody] UpdateCategoryDto category)
        {
            return ApiResult(await _solutionService.UpdateCategoryAsync(category));
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
        public async Task<IActionResult> AddSolutionLinkAsync([FromBody] NewSolutionLinkDto link)
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
        public async Task<IActionResult> AddSolutionPlanAsync([FromBody] NewPlanDto plan)
        {
            var result = await _solutionService.AddPlanAsync(plan);
            return ApiResult(result);
        }

        /// <summary>
        /// Добавление отзыва к решению
        /// </summary>
        /// <param name="review"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("reviews")]
        public async Task<IActionResult> AddSolutionReviewAsync([FromBody] NewReviewDto review)
        {
            var result = await _solutionService.AddReviewAsync(review);
            return ApiResult(result);
        }

        /// <summary>
        /// Получить решения по фильтру
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost]
        [Route("filter")]
        public async Task<IActionResult> GetSolutionsByFilterAsync([FromBody] SolutionFilter filter)
        {
            var sols = await _solutionService.GetByFilterAsync(filter);
            return ApiResult(sols);
        }
    }
}
