using CloudJ.Client.Options;
using CloudJ.Contracts;
using CloudJ.Contracts.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace CloudJ.Client.Clients
{
    public interface IIdentityClient
    {
        Task<ApiResponse<UserDto>> GetUserInfoAsync(string id);
        Task<ApiResponse<UserDto>> UpdateUserInfoAsync(UserDto user);
    }
    public class IdentityClient : ApiClient, IIdentityClient
    {
        private readonly IdentityClientOptions _identityOptions;
        public IdentityClient(
            HttpClient client,
            IHttpContextAccessor accessor,
            IOptions<IdentityClientOptions> identityOptions
            ) : base(client, accessor)
        {
            _identityOptions = identityOptions.Value;

        }

        public Task<ApiResponse<UserDto>> GetUserInfoAsync(string id)
        {
            if (id == null)
                throw new ArgumentNullException(nameof(id));
            return GetAsync<ApiResponse<UserDto>>($"{_identityOptions.UserInfoByIdUrl}/{id}");
        }

        public Task<ApiResponse<UserDto>> UpdateUserInfoAsync(UserDto user)
        {
            if (user.Id == null)
                throw new ArgumentNullException(nameof(user.Id));
            return PostAsync<UserDto, ApiResponse<UserDto>>(_identityOptions.UpdateUserInfoUrl, user);
        }
    }
}
