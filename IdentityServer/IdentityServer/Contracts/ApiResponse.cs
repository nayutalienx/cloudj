using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Contracts
{
    public sealed class ApiResponse
    {
        public bool HasErrors => Errors.Any();

        public List<string> Errors { get; set; } = new List<string>();

        public ApiResponse() { }

        public ApiResponse(string error) => Errors.Add(error);
    }

    public sealed class ApiResponse<TData>
    {
        public bool HasErrors => Errors.Any();

        public TData Data { get; set; }

        public List<string> Errors { get; set; } = new List<string>();

        public ApiResponse() { }

        public ApiResponse(TData data) => Data = data;
    }
}
