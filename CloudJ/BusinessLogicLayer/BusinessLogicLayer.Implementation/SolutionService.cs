using AutoMapper;
using BusinessLogicLayer.Abstraction;
using CloudJ.Contracts.DTOs.SolutionDtos;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Models.Solution;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Implementation
{
    public class SolutionService : ISolutionService
    {
        private readonly ISolutionRepository _solutionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        /// <summary>
        /// Конструктор, подключение зависимостей
        /// </summary>
        /// <param name="solutionRepository"></param>
        /// <param name="categoryRepository"></param>
        /// <param name="mapper"></param>
        public SolutionService(
            ISolutionRepository solutionRepository,
            ICategoryRepository categoryRepository,
            IMapper mapper)
        {
            _solutionRepository = solutionRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }
        
        /// <summary>
        /// Добавление категории
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<CategoryDto> AddCategoryAsync(CategoryDto dto)
        {
            var result = await _categoryRepository.AddAsync(_mapper.Map<Category>(dto));
            await _categoryRepository.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(result);
        }

        /// <summary>
        /// Добавление отзыва
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<ReviewDto> AddReviewAsync(ReviewDto dto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Создание солюшна
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<SolutionDto> CreateAsync(SolutionDto dto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получение всех солюшнов
        /// </summary>
        /// <returns></returns>
        public Task<IReadOnlyCollection<SolutionDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получение всех категорий
        /// </summary>
        /// <returns></returns>
        public Task<IReadOnlyCollection<CategoryDto>> GetAllCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получение солюшнов по фильтру
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public Task<IReadOnlyCollection<SolutionDto>> GetByFilter(SolutionFilter filter)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Удалить солюшн
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task RemoveAsync(SolutionDto dto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Обновить солюшн
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public Task<SolutionDto> UpdateAsync(SolutionDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
