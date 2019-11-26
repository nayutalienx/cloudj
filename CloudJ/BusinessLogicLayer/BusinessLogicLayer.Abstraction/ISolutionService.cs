using CloudJ.Contracts.DTOs.SolutionDtos;
using CloudJ.Contracts.DTOs.SolutionDtos.Category;
using CloudJ.Contracts.DTOs.SolutionDtos.Review;
using CloudJ.Contracts.DTOs.SolutionDtos.Solution;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Abstraction
{
    public interface ISolutionService
    {
        /// <summary>
        /// Добавить новое решение
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<SolutionDto> CreateAsync(SolutionDto dto);
        /// <summary>
        /// Получить все решения
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyCollection<SolutionDto>> GetAllAsync();
        /// <summary>
        /// Обновить данные о решение
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<SolutionDto> UpdateAsync(SolutionDto dto);
        /// <summary>
        /// Удалить решение
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task RemoveAsync(SolutionDto dto);
        /// <summary>
        /// Получить по фильтру
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        Task<IReadOnlyCollection<SolutionDto>> GetByFilter(SolutionFilter filter);
        /// <summary>
        /// Добавить отзыв 
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<ReviewDto> AddReviewAsync(ReviewDto dto);
        /// <summary>
        /// Добавить категорию
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        Task<CategoryDto> AddCategoryAsync(CategoryDto dto);
        /// <summary>
        /// Получить все категории
        /// </summary>
        /// <returns></returns>
        Task<IReadOnlyCollection<CategoryDto>> GetAllCategoriesAsync();
        
        
    }
}
