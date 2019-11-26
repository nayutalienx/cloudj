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
        private readonly IPhotoRepository _photoRepository;
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
            IPhotoRepository photoRepository,
            IMapper mapper)
        {
            _solutionRepository = solutionRepository;
            _categoryRepository = categoryRepository;
            _photoRepository = photoRepository;
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
            var sol = await _solutionRepository.GetAsync(dto.SolutionId);
            var link = _mapper.Map<SolutionLink>(dto);
            sol.SolutionLinks.Add(link);
            await _solutionRepository.SaveChangesAsync();
            return _mapper.Map<SolutionLinkDto>(link);
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
        public async Task<SolutionDto> UpdateAsync(UpdateSolutionDto dto)
        {
            var sol = await _solutionRepository.GetAsync(dto.Id);
            if(dto.Name != null)
                sol.Name = dto.Name;
            if(dto.Description != null)
                sol.Description = dto.Description;
            if(dto.CategoryId != null)
                sol.CategoryId = (long)dto.CategoryId;
            if (dto.Cloud != null)
            {
                sol.Cloud.Name = dto.Cloud.Name;
                sol.Cloud.Url = dto.Cloud.Url;
                if(dto.Cloud.Container != null)
                {
                    sol.Cloud.Container.Image = dto.Cloud.Container.Image;
                }
            }

            ICollection<Photo> photos = new List<Photo>();

            if (dto.Photos?.Length > 0)
            {
                if (sol.Photos?.Count > 0)
                {
                    foreach (Photo ph in sol.Photos)
                        photos.Add(new Photo 
                        {
                            Id = ph.Id,
                            Solution = ph.Solution,
                            SolutionId = ph.SolutionId,
                            Data = ph.Data,
                            Type = ph.Type
                        });
                }
                sol.Photos = _mapper.Map<ICollection<Photo>>(dto.Photos);
                //await _solutionRepository.UpdateAsync(sol);
            }

            await _solutionRepository.SaveChangesAsync();

            if (photos.Count > 0)
            {
                try
                {
                    foreach (Photo ph in photos)
                        await _photoRepository.RemoveAsync(ph);
                    await _photoRepository.SaveChangesAsync();
                }
                catch (Exception ex) { }
            }
            return _mapper.Map<SolutionDto>(sol);


        }
    }
}
