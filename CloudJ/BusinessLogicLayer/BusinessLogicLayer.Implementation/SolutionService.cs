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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Implementation
{
    public class SolutionService : ISolutionService
    {
        private readonly ISolutionRepository _solutionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IPhotoRepository _photoRepository;
        private readonly ISolutionLinkRepository _solutionLinkRepository;
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
            ISolutionLinkRepository solutionLinkRepository,
            IMapper mapper)
        {
            _solutionRepository = solutionRepository;
            _categoryRepository = categoryRepository;
            _photoRepository = photoRepository;
            _solutionLinkRepository = solutionLinkRepository;
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
        public async Task<ReviewDto> AddReviewAsync(NewReviewDto dto)
        {
            var sol = await _solutionRepository.GetAsync(dto.SolutionId);
            var review = _mapper.Map<Review>(dto);
            sol.Reviews.Add(review);
            if(sol.Rate == 0)
            {
                sol.Rate = review.Rate;
            }
            else
            {
                byte temp = (byte)(sol.Rate + dto.Rate);
                temp = (byte)(temp / 2);
                sol.Rate = temp;
            }
            await _solutionRepository.SaveChangesAsync();
            return _mapper.Map<ReviewDto>(review);
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
            var sol = await _solutionRepository.GetAsync(dto.SolutionId);
            var plan = _mapper.Map<Plan>(dto);
            sol.Plans.Add(plan);
            await _solutionRepository.SaveChangesAsync();
            return _mapper.Map<PlanDto>(plan);
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
        public async Task<IReadOnlyCollection<SolutionDto>> GetByFilterAsync(SolutionFilter filter)
        {
            var sols = await _solutionRepository.GetAllAsync();

            if (filter.CategoryId != null)
                sols = sols.Where(s => s.CategoryId == filter.CategoryId);
            if (filter.DeveloperId != null)
                sols = sols.Where(s => s.UserId.Equals(filter.DeveloperId));

            if (filter.SolutionId != null)
            {
                sols = sols.Where(s => s.Id == filter.SolutionId);
            }
            else
            {
                sols = sols.Select(s =>
                    new Solution
                    {
                        Id = s.Id,
                        Name = s.Name,
                        Photos = (s.Photos.Any()) ? new List<Photo> { s.Photos.FirstOrDefault(x => x.Type.Equals("Logo")) } : null,
                        Rate = s.Rate,
                        Description = s.Description,
                        Plans = s.Plans
                    }
                );
            }


            sols = sols.Skip((filter.Page - 1) * filter.Size).Take(filter.Size);
            return _mapper.Map<IReadOnlyCollection<SolutionDto>>(sols.ToArray());
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
        /// <summary>
        /// Удаление категории
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task RemoveCategoryAsync(RemoveCategoryDto dto)
        {
            var cat = await _categoryRepository.GetAsync(dto.Id);
            await _categoryRepository.RemoveAsync(cat);
            await _categoryRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Обновить категорию
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<CategoryDto> UpdateCategoryAsync(UpdateCategoryDto dto)
        {
            var cat = await _categoryRepository.GetAsync(dto.Id);
            if (dto.Name != null)
                cat.Name = dto.Name;
            if (dto.Description != null)
                cat.Description = dto.Description;
            if (dto.ParentCategoryId != null)
                cat.ParentCategoryId = dto.ParentCategoryId;
            if (dto.Logo != null)
            {
                if (cat.Logo != null)
                    cat.Logo.Data = dto.Logo.Data;
                else
                    cat.Logo = _mapper.Map<Photo>(dto.Logo);
            }

            await _categoryRepository.SaveChangesAsync();
            return _mapper.Map<CategoryDto>(cat);
        }
        /// <summary>
        /// Удалить ссылку разработчика
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task RemoveSolutionLink(RemoveSolutionLinkDto dto)
        {
            var link = await _solutionLinkRepository.GetAsync(dto.Id);
            await _solutionLinkRepository.RemoveAsync(link);
            await _solutionLinkRepository.SaveChangesAsync();
        }
    }
}
