using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CloudJ.API.Results;
using Microsoft.AspNetCore.Mvc;

namespace CloudJ.API.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class ValuesController : ApiController
    {
        // GET api/values
        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return ApiResult("User");
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            return ApiResult("get");
        }

        // POST api/values
        [HttpPost]
        public async Task Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public async Task Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public async Task Delete(int id)
        {
        }
    }
}
