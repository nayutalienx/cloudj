using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CloudJ.Client.Options;
using CloudJ.Contracts;
using CloudJ.Contracts.DTOs.SolutionDtos.Category;
using CloudJ.Contracts.DTOs.SolutionDtos.Plan;
using CloudJ.Contracts.DTOs.SolutionDtos.Review;
using CloudJ.Contracts.DTOs.SolutionDtos.Solution;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CloudJ.Client.Clients
{
    public interface ISolutionApiClient
    {
        Task<ApiResponse<SolutionDto>> UpdateSolutionAsync(UpdateSolutionDto dto);
        Task<ApiResponse<CategoryDto>> UpdateCategoryAsync(UpdateCategoryDto dto);
        Task<ApiResponse<SolutionDto>> AddSolutionAsync(NewSolutionDto dto);
        Task<ApiResponse> RemoveSolutionAsync(RemoveSolutionDto dto);
        Task<ApiResponse> RemoveCategoryAsync(RemoveCategoryDto dto);
        Task<ApiResponse<IReadOnlyCollection<CategoryDto>>> GetAllCategoriesAsync();
        Task<ApiResponse<CategoryDto>> AddCategoryAsync(NewCategoryDto dto);
        Task<ApiResponse<SolutionLinkDto>> AddSolutionLinkAsync(NewSolutionLinkDto dto);
        Task<ApiResponse<PlanDto>> AddSolutionPlanAsync(NewPlanDto dto);
        Task<ApiResponse<ReviewDto>> AddReviewAsync(NewReviewDto dto);
        Task<ApiResponse<IReadOnlyCollection<SolutionDto>>> GetByFilterAsync(SolutionFilter filter);


    }
    public class SolutionApiClient : ApiClient, ISolutionApiClient
    {
        private readonly SolutionApiClientOptions _clientOptions;
        public SolutionApiClient(HttpClient client, IHttpContextAccessor accessor, IOptions<SolutionApiClientOptions> clientOptions) : base(client, accessor)
        {
            _clientOptions = clientOptions.Value;
        }

        public Task<ApiResponse<CategoryDto>> AddCategoryAsync(NewCategoryDto dto)
        {
            return PostAsync<NewCategoryDto, ApiResponse<CategoryDto>>(_clientOptions.AddCategoryUrl, dto);
        }

        public Task<ApiResponse<ReviewDto>> AddReviewAsync(NewReviewDto dto)
        {
            return PostAsync<NewReviewDto, ApiResponse<ReviewDto>>(_clientOptions.AddReviewUrl, dto);
        }

        public Task<ApiResponse<SolutionDto>> AddSolutionAsync(NewSolutionDto dto)
        {
            return PostAsync<NewSolutionDto, ApiResponse<SolutionDto>>(_clientOptions.AddSolutionUrl, dto);
        }

        public Task<ApiResponse<SolutionLinkDto>> AddSolutionLinkAsync(NewSolutionLinkDto dto)
        {
            return PostAsync<NewSolutionLinkDto, ApiResponse<SolutionLinkDto>>(_clientOptions.AddSolutionLinkUrl, dto);
        }

        public Task<ApiResponse<PlanDto>> AddSolutionPlanAsync(NewPlanDto dto)
        {
            return PostAsync<NewPlanDto, ApiResponse<PlanDto>>(_clientOptions.AddSolutionPlanUrl, dto);
        }

        public Task<ApiResponse<IReadOnlyCollection<CategoryDto>>> GetAllCategoriesAsync()
        {
            return GetAsync<ApiResponse<IReadOnlyCollection<CategoryDto>>>(_clientOptions.GetAllCategoriesUrl);
        }

        public Task<ApiResponse<IReadOnlyCollection<SolutionDto>>> GetByFilterAsync(SolutionFilter filter)
        {
            return PostAsync<SolutionFilter, ApiResponse<IReadOnlyCollection<SolutionDto>>>(_clientOptions.GetSolutionsByFilterUrl, filter);
        }

        public Task<ApiResponse> RemoveCategoryAsync(RemoveCategoryDto dto)
        {
            return DeleteAsync<RemoveCategoryDto, ApiResponse>(_clientOptions.DeleteCategoryUrl, dto);
        }

        public Task<ApiResponse> RemoveSolutionAsync(RemoveSolutionDto dto)
        {
            return DeleteAsync<RemoveSolutionDto, ApiResponse>(_clientOptions.DeleteSolutionUrl, dto);
        }

        public Task<ApiResponse<CategoryDto>> UpdateCategoryAsync(UpdateCategoryDto dto)
        {
            return PutAsync<UpdateCategoryDto, ApiResponse<CategoryDto>>(_clientOptions.UpdateCategoryUrl, dto);
        }

        public Task<ApiResponse<SolutionDto>> UpdateSolutionAsync(UpdateSolutionDto dto)
        {
            return PutAsync<UpdateSolutionDto, ApiResponse<SolutionDto>>(_clientOptions.UpdateSolutionUrl, dto);
        }

    }
}
