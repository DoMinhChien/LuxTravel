using System;
using MediatR;

namespace LuxTravel.Api.Core.Commands
{
    public class CreateRoomCommand : IRequest<bool>
    {
        public Guid HotelId { get; set; }
        public string Name { get; set; }
        public Guid RoomTypeId { get; set; }
        public int RoomFloor { get; set; }
        public int RoomNumber { get; set; }
        public decimal CurrentPrice { get; set; }
        public int Quantity { get; set; }
    }
}
