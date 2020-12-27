using System;
using LuxTravel.Model.Dtos;
using MediatR;

namespace LuxTravel.Api.Core.Queries
{
    public class GetDetailHotelQuery : IRequest<HotelDto>
    {
        public Guid Id { get; set; }
    }
}
