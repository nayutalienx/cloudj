﻿using BusinessLogicLayer.Abstraction;
using CloudJ.Contracts.DTOs.SolutionDtos;
using CloudJ.Contracts.DTOs.SolutionDtos.Category;
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
    }
}
