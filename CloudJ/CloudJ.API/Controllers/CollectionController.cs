using BusinessLogicLayer.Abstraction;
using CloudJ.Contracts.DTOs.CollectionDtos;
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
    public class CollectionController : ApiController
    {
        private readonly ICollectionService _collectionService;
        public CollectionController(ICollectionService collectionService)
        {
            _collectionService = collectionService;
        }

        /// <summary>
        /// Добавить подборку
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> AddCollectionAsync(NewCollectionDto dto)
        {
            var result = await _collectionService.AddCollectionAsync(dto);
            return ApiResult(result);
        }
        /// <summary>
        /// Удалить подборку
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpDelete]
        public async Task<IActionResult> DeleteCollectionAsync(RemoveCollectionDto dto)
        {
            await _collectionService.RemoveCollectionAsync(dto);
            return ApiResult("Deleted.");
        }

        /// <summary>
        /// Получить все подборки
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetAllCollectionsAsync()
        {
            return ApiResult(await _collectionService.GetAllAsync());
        }
        /// <summary>
        /// Получить коллекции по фильтру
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [Route("filter")]
        public async Task<IActionResult> GetCollectionsByFilter([FromQuery]CollectionFilter filter)
        {
            return ApiResult(await _collectionService.GetAllByFilterAsync(filter));
        }

        /// <summary>
        /// Обновить подборку
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        [HttpPut]
        public async Task<IActionResult> UpdateCollectionAsync([FromBody]UpdateCollectionDto dto)
        {
            return ApiResult(await _collectionService.UpdateCollectionAsync(dto));
        }

    }
}
