using AutoMapper;
using BusinessLogicLayer.Abstraction;
using CloudJ.Contracts.DTOs.SolutionDtos;
using CloudJ.Contracts.DTOs.SolutionDtos.Category;
using CloudJ.Contracts.DTOs.SolutionDtos.Plan;
using CloudJ.Contracts.DTOs.SolutionDtos.Review;
using CloudJ.Contracts.DTOs.SolutionDtos.Solution;
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
        public async Task<CategoryDto> AddCategoryAsync(NewCategoryDto dto)
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
        public async Task<SolutionDto> CreateAsync(NewSolutionDto dto)
        {
            var result = await _solutionRepository.AddAsync(_mapper.Map<Solution>(dto));
            await _solutionRepository.SaveChangesAsync();
            return _mapper.Map<SolutionDto>(result);
        }
        /// <summary>
        /// Добавить план к решению
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<PlanDto> AddPlanAsync(NewPlanDto dto)
        {
            throw new NotImplementedException();
        }
        /// <summary>
        /// Добавление ссылок от разработчика для решения
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<SolutionLinkDto> AddSolutionLink(NewSolutionLinkDto dto)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Получение всех солюшнов
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<SolutionDto>> GetAllAsync()
        {
            var result = await _solutionRepository.GetAllAsync();
            return _mapper.Map<IReadOnlyCollection<SolutionDto>>(result);
        }

        /// <summary>
        /// Получение всех категорий
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<CategoryDto>> GetAllCategoriesAsync()
        {
            var cats = await _categoryRepository.GetAllAsync();
            return _mapper.Map<IReadOnlyCollection<CategoryDto>>(cats);
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
        public async Task RemoveAsync(RemoveSolutionDto dto)
        {
            await _solutionRepository.RemoveAsync(_mapper.Map<Solution>(dto));
            await _solutionRepository.SaveChangesAsync();
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
