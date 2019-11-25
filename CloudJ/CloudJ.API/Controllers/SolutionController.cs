using BusinessLogicLayer.Abstraction;
using CloudJ.Contracts.DTOs.SolutionDtos;
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
        [HttpPost]
        public async Task<IActionResult> AddCategoryAsync([FromBody] CategoryDto category)
        {
            return ApiResult(_solutionService.AddCategoryAsync(category));
        }
    }
}
