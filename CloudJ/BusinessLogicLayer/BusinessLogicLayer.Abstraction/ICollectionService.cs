using CloudJ.Contracts.DTOs.CollectionDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstraction
{
    public interface ICollectionService
    {
        /// <summary>
        /// Добавить подборку
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<CollectionDto> AddCollectionAsync(NewCollectionDto dto);
        /// <summary>
        /// Удалить подборку
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task RemoveCollectionAsync(RemoveCollectionDto dto);
        /// <summary>
        /// Получить все подборки
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyCollection<CollectionDto>> GetAllAsync();
        /// <summary>
        /// Получить все подборки по фильтру
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<CollectionDto>> GetAllByFilterAsync(CollectionFilter filter);
        /// <summary>
        /// Обновить подборку
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<CollectionDto> UpdateCollectionAsync(UpdateCollectionDto dto);
    }
}
