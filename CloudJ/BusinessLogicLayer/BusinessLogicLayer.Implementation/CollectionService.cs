using AutoMapper;
using BusinessLogicLayer.Abstraction;
using CloudJ.Contracts.DTOs.CollectionDtos;
using DataAccessLayer.Abstraction;
using DataAccessLayer.Models.Collection;
using DataAccessLayer.Models.Solution;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Implementation
{
    public class CollectionService : ICollectionService
    {
        private readonly ICollectionRepository _collectionRepository;
        private readonly IMapper _mapper;
        public CollectionService(ICollectionRepository collectionRepository, IMapper mapper)
        {
            _collectionRepository = collectionRepository;
            _mapper = mapper;
        }
        /// <summary>
        /// Добавить коллекцию
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<CollectionDto> AddCollectionAsync(NewCollectionDto dto)
        {
            var result = await _collectionRepository.AddAsync(_mapper.Map<Collection>(dto));
            await _collectionRepository.SaveChangesAsync();
            return _mapper.Map<CollectionDto>(result);
        }
        /// <summary>
        /// Получить все подборки
        /// </summary>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<CollectionDto>> GetAllAsync()
        {
            var result = await _collectionRepository.GetAllAsync();
            return _mapper.Map<IReadOnlyCollection<CollectionDto>>(result); 
        }
        /// <summary>
        /// Получить все подборки по фильтру
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<IReadOnlyCollection<CollectionDto>> GetAllByFilterAsync(CollectionFilter filter)
        {
            var result = await _collectionRepository.GetAllAsync();
            if (filter.Id != null)
                result = result.Where(c => c.Id == filter.Id);

            result = result.Skip((filter.Page - 1) * filter.Size).Take(filter.Size);
            return _mapper.Map<IReadOnlyCollection<CollectionDto>>(result.ToArray());

            

        }

        /// <summary>
        /// Удалить коллекцию
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task RemoveCollectionAsync(RemoveCollectionDto dto)
        {
            var result = await _collectionRepository.GetAsync(dto.Id);
            await _collectionRepository.RemoveAsync(result);
            await _collectionRepository.SaveChangesAsync();
        }

        /// <summary>
        /// Обновить подборку
        /// </summary>
        /// <param name="dto"></param>
        /// <returns></returns>
        public async Task<CollectionDto> UpdateCollectionAsync(UpdateCollectionDto dto)
        {
            var result = await _collectionRepository.GetAsync(dto.Id);
            if (dto.Name != null)
                result.Name = dto.Name;
            if (dto.Description != null)
                result.Description = dto.Description;
            if (dto.Preview != null)
                result.Preview = dto.Preview;
            if(dto.PhotoPreview != null)
            {
                if (result.PhotoPreview == null)
                    result.PhotoPreview = _mapper.Map<Photo>(dto.PhotoPreview);
                else
                    result.PhotoPreview.Data = dto.PhotoPreview.Data;
            }
            if(dto.Solutions != null)
            {
                result.Solutions = _mapper.Map<ICollection<TruncSolution>>(dto.Solutions);
            }
            await _collectionRepository.SaveChangesAsync();
            return _mapper.Map<CollectionDto>(result);

        }
    }
}
