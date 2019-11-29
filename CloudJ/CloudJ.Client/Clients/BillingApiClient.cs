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
        Task<ApiResponse<BalanceDto>> AddBalanceAsync(NewBalanceDto dto);
        Task<ApiResponse<BalanceDto>> UpdateBalanceAsync(UpdateBalanceDto dto);
        Task<ApiResponse<IReadOnlyCollection<BalanceDto>>> GetBalanceByFilterAsync(BalanceFilter filter);
    }
    public class BillingApiClient : ApiClient , IBillingApiClient
    {
        private readonly BillingApiClientOptions _clientOptions;
        public BillingApiClient(HttpClient client, IHttpContextAccessor accessor, BillingApiClientOptions clientOptions) : base(client, accessor)
        {
            _clientOptions = clientOptions;
        }

        public Task<ApiResponse<BalanceDto>> AddBalanceAsync(NewBalanceDto dto)
        {
            return PostAsync<NewBalanceDto, ApiResponse<BalanceDto>>(_clientOptions.AddBalanceUrl, dto);
        }

        public Task<ApiResponse<OrderDto>> AddOrderAsync(NewOrderDto dto)
        {
            return PostAsync<NewOrderDto, ApiResponse<OrderDto>>(_clientOptions.MakeOrderUrl, dto);
        }

        public Task<ApiResponse<IReadOnlyCollection<BalanceDto>>> GetBalanceByFilterAsync(BalanceFilter filter)
        {
            return GetAsync<ApiResponse<IReadOnlyCollection<BalanceDto>>>($"{_clientOptions.GetBalanceByFilterUrl}?id={filter.Id}&userid={filter.UserId}");
        }

        public Task<ApiResponse<IReadOnlyCollection<OrderDto>>> GetByFilterAsync(OrderFilter filter)
        {
            return PostAsync<OrderFilter, ApiResponse<IReadOnlyCollection<OrderDto>>>(_clientOptions.GetOrdersByFilterUrl, filter);
        }

        public Task<ApiResponse<BalanceDto>> UpdateBalanceAsync(UpdateBalanceDto dto)
        {
            return PutAsync<UpdateBalanceDto, ApiResponse<BalanceDto>>(_clientOptions.UpdateBalanceUrl, dto);
        }
    }
}
