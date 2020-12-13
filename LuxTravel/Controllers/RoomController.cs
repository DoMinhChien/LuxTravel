using System;
using CommonFunctionality.Api;
using Microsoft.AspNetCore.Mvc;

namespace LuxTravel.Api.Controllers
{
    [Route("api/room")]

    public class RoomController : ApiControllerBase
    {
        public RoomController(IServiceProvider serviceProvider): base(serviceProvider)
        {
            
        }

        //[HttpGet("hotels")]
        //public async Task<IEnumerable<RoomDto>> GetAllRoomByHotel([FromQuery] GetAllHotelsQuery model)
        //{
        //    var result = await _mediator.Send(model);

        //    return result;
        //}
    }
}
