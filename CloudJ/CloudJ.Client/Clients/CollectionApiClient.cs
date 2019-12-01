using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CloudJ.Client.Options;
using CloudJ.Contracts;
using CloudJ.Contracts.DTOs.CollectionDtos;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace CloudJ.Client.Clients
{
    public interface ICollectionApiClient
    {
        Task<ApiResponse<IReadOnlyCollection<CollectionDto>>> GetByFilterAsync(CollectionFilter filter);
        Task<ApiResponse<CollectionDto>> AddCollectionAsync(NewCollectionDto dto);
        Task<ApiResponse> RemoveCollectionAsync(RemoveCollectionDto dto);
        Task<ApiResponse<CollectionDto>> UpdateCollectionAsync(UpdateCollectionDto dto);
       

    }
    public class CollectionApiClient : ApiClient, ICollectionApiClient
    {
        private readonly CollectionApiClientOptions _clientOptions;
        public CollectionApiClient(HttpClient client, IHttpContextAccessor accessor, IOptions<CollectionApiClientOptions> clientOptions) : base(client, accessor)
        {
            _clientOptions = clientOptions.Value;
        }

        public Task<ApiResponse<CollectionDto>> AddCollectionAsync(NewCollectionDto dto)
        {
            return PostAsync<NewCollectionDto, ApiResponse<CollectionDto>>(_clientOptions.AddCollectionUrl, dto);
        }

        public Task<ApiResponse<IReadOnlyCollection<CollectionDto>>> GetByFilterAsync(CollectionFilter filter)
        {
            return GetAsync<ApiResponse<IReadOnlyCollection<CollectionDto>>>($"{_clientOptions.GetCollectionByFilterUrl}?id={filter.Id}");

        }

        public Task<ApiResponse> RemoveCollectionAsync(RemoveCollectionDto dto)
        {
            return DeleteAsync<RemoveCollectionDto, ApiResponse>(_clientOptions.RemoveCollectionUrl, dto);
        }

        public Task<ApiResponse<CollectionDto>> UpdateCollectionAsync(UpdateCollectionDto dto)
        {
            return PutAsync<UpdateCollectionDto, ApiResponse<CollectionDto>>(_clientOptions.UpdateCollectionUrl, dto);
        }
    }
}
