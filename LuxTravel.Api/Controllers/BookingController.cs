using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonFunctionality.Api;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Model.Dtos;
using MediatR;

namespace LuxTravel.Api.Controllers
{
    [Route("api/booking")]

    public class BookingController : ApiControllerBase
    {
        public BookingController(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }
        [HttpGet("bookings")]
        public async Task<IEnumerable<BookingDto>> Get([FromQuery] GetAllBookingsQuery model)
        {
            var result = await SendRequestAsync(model);

            return result;
        }



    }
}
