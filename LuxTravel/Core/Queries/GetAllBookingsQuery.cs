using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LuxTravel.Models.Dtos;
using MediatR;

namespace LuxTravel.Api.Core.Queries
{
    public class GetAllBookingsQuery : IRequest<IEnumerable<BookingDto>>
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public Guid CityId { get; set; }
        public int RoomCount { get; set; }
        public int GuestCount { get; set; }
    }
}
