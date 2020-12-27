using System;
using MediatR;

namespace LuxTravel.Api.Core.Commands
{
    public class CreateBookingCommand : IRequest<bool>
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public Guid GuestId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int RoomCount { get; set; }

    }
}
