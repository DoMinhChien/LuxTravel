using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonFunctionality.Api;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Models.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LuxTravel.Api.Controllers
{
    [Route("api/booking")]

    public class BookingController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public BookingController(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
        [HttpGet("bookings")]
        public async Task<IEnumerable<BookingDto>> Get([FromQuery] GetAllBookingsQuery model)
        {
            var result = await _mediator.Send(model);

            return result;
        }
        
        //[HttpPost("")]
        //public async Task<bool> CreateBooking([FromBody] )


    }
}
