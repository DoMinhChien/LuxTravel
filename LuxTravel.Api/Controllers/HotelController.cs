using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommonFunctionality.Api;
using CommonFunctionality.Helper;
using LuxTravel.Api.Core.Commands;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Model.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace LuxTravel.Api.Controllers
{
    [Authorize]

    public class HotelController : ApiControllerBase
    {

        public HotelController(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
        [AllowAnonymous]
        [HttpGet("api/hotels")]
        public async Task<IEnumerable<HotelDto>> Get([FromQuery] GetAllHotelsQuery model)
        {
            return await SendRequestAsync(model);
        }
        [AllowAnonymous]
        [HttpGet("api/hotels/{id}")]
        public async Task<HotelDetailDto> Get(Guid id)
        {
            var result = await SendRequestAsync(new GetDetailHotelQuery() { Id = id });

            return result;
        }
        [HttpPost("api/hotel")]
        public async Task<bool> Create(CreateHotelCommand command)
        {
            var result = await SendRequestAsync(command);

            return result;
        }
        [HttpPost("api/hotel/rating")]
        public async Task<bool> RateHotel(CreateHotelRatingCommand command)
        {
            var result = await SendRequestAsync(command);

            return result;
        }
        
    }
}
