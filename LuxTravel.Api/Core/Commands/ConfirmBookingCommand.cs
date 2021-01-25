using System;
using MediatR;

namespace LuxTravel.Api.Core.Commands
{
    public class ConfirmBookingCommand : IRequest<bool>
    {
        public string PaymentId { get; set; }
        public Guid BookingId { get; set; }
        public string Error { get; set; }
    }
}
