using System;
using LuxTravel.Model.Dtos;
using MediatR;

namespace LuxTravel.Api.Core.Queries
{
    public class GetDetailHotelQuery : IRequest<HotelDetailDto>
    {
        public Guid Id { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int GuestCount { get; set; }
    }
}
