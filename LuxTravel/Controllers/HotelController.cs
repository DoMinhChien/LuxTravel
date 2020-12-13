using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommonFunctionality.Api;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Models.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LuxTravel.Api.Controllers
{
    //TO DO : Create API controller base
    [Route("api/hotel")]
    public class HotelController : ApiControllerBase
    {
        public HotelController(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
        //[AllowAnonymous]
        [HttpGet("hotels")]
        public async Task<IEnumerable<HotelDto>> Get([FromQuery] GetAllHotelsQuery model)
        {
            return await SendRequestAsync(model);
        }

        [HttpGet("{id}")]
        public async Task<HotelDto> Get(Guid id)
        {
            var result = await SendRequestAsync(new GetDetailHotelQuery() { Id =  id});
    
            return result;
        }
    }
}
