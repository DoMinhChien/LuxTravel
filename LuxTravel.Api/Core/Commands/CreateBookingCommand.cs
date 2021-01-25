using System;
using LuxTravel.Model.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LuxTravel.Api.Core.Commands
{
    public class CreateBookingCommand : IRequest<PaymentConfirmDto>
    {
        public Guid HotelId { get; set; }
        public DateTime  DateFrom { get; set; }
        public DateTime  DateTo { get; set; }
        public int GuestCount { get; set; }
        
        public SelectedRoomDto SelectedRoom { get; set; }  
        public int RoomCount { get; set; }

    }
}
