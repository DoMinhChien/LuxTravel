using System;
using LuxTravel.Model.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LuxTravel.Api.Core.Queries
{
    public class GetBookingDetailQuery : IRequest<BookingDto>
    {
        public Guid HotelId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int GuestCount { get; set; }
        public SelectedRoomDto SelectedRoom { get; set; }
    }
}
