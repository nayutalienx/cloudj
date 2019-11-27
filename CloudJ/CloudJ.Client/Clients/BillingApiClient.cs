using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using CloudJ.Client.Options;
using CloudJ.Contracts;
using CloudJ.Contracts.DTOs.OrderDtos;
using Microsoft.AspNetCore.Http;

namespace CloudJ.Client.Clients
{
    public interface IBillingApiClient 
    {
        Task<ApiResponse<OrderDto>> AddOrderAsync(NewOrderDto dto);
        Task<ApiResponse<IReadOnlyCollection<OrderDto>>> GetByFilterAsync(OrderFilter filter);
    }
    public class BillingApiClient : ApiClient , IBillingApiClient
    {
        private readonly BillingApiClientOptions _clientOptions;
        public BillingApiClient(HttpClient client, IHttpContextAccessor accessor, BillingApiClientOptions clientOptions) : base(client, accessor)
        {
            _clientOptions = clientOptions;
        }

        public Task<ApiResponse<OrderDto>> AddOrderAsync(NewOrderDto dto)
        {
            return PostAsync<NewOrderDto, ApiResponse<OrderDto>>(_clientOptions.MakeOrderUrl, dto);
        }

        public Task<ApiResponse<IReadOnlyCollection<OrderDto>>> GetByFilterAsync(OrderFilter filter)
        {
            return PostAsync<OrderFilter, ApiResponse<IReadOnlyCollection<OrderDto>>>(_clientOptions.GetOrdersByFilterUrl, filter);
        }
    }
}
