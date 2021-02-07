using LuxTravel.Model.Dtos;
using MediatR;

namespace LuxTravel.Api.Core.Queries
{
    public class GetBookingHistoryQuery : IRequest<BookingHistoryDto>
    {
    }
}
