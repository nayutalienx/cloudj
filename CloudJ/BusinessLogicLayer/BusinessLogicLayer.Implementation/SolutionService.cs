using BusinessLogicLayer.Abstraction;
using CloudJ.Contracts.DTOs.SolutionDtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicLayer.Implementation
{
    public class SolutionService : ISolutionService
    {
        public Task<CategoryDto> AddCategoryAsync(CategoryDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<ReviewDto> AddReviewAsync(ReviewDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<SolutionDto> CreateAsync(SolutionDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<SolutionDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<CategoryDto>> GetAllCategoriesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyCollection<SolutionDto>> GetByFilter(SolutionFilter filter)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(SolutionDto dto)
        {
            throw new NotImplementedException();
        }

        public Task<SolutionDto> UpdateAsync(SolutionDto dto)
        {
            throw new NotImplementedException();
        }
    }
}
