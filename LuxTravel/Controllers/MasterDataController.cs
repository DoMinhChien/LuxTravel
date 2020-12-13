using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonFunctionality.Api;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LuxTravel.Api.Controllers
{
    [Route("api/master-data")]
    [ApiController]

    public class MasterDataController : ApiControllerBase
    {
        public MasterDataController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            
        }

        [HttpGet("countries")]
        public async Task<IEnumerable<SelectedObjectDto>> GetCity([FromQuery] GetAllCitiesQuery model)
        {
            return await SendRequestAsync(model);
        }

    }
}
