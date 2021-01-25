using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using CommonFunctionality.Api;
using LuxTravel.Api.Core.Commands;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Api.Core.Services;
using LuxTravel.Model.Dtos;
using Microsoft.AspNetCore.Authorization;

namespace LuxTravel.Api.Controllers
{
    [Route("api/booking")]
    [Authorize]
    public class BookingController : ApiControllerBase
    {
        private readonly IPaymentService _paymentService;
        public BookingController(IServiceProvider serviceProvider,
            IPaymentService paymentService) : base(serviceProvider)
        {
            _paymentService = paymentService;
        }
        [HttpGet("detail")]
        public async Task<BookingDto> Get([FromQuery] GetBookingDetailQuery model)
        {
            var result = await SendRequestAsync(model);
            return result;
        }

        [HttpPost("create-booking")]
        public async Task<PaymentConfirmDto> CreateBooking([FromBody] CreateBookingCommand model)
        {
            var result = await SendRequestAsync(model);

            return result;
        }

        [HttpPost("confirm-booking")]
        public async Task<bool> ConfirmBooking([FromBody] ConfirmBookingCommand model)
        {
            var result = await SendRequestAsync(model);

            return result;
        }

    }
}
