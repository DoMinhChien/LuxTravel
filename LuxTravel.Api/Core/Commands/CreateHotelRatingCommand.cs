using System;
using MediatR;

namespace LuxTravel.Api.Core.Commands
{
    public class CreateHotelRatingCommand : IRequest<bool>
    {
        public Guid HotelId { get; set; }
        public Decimal Point { get; set; }
    }
}
