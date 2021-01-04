using System;
using System.Collections.Generic;
using CommonFunctionality.Helper;
using LuxTravel.Model.Dtos;
using MediatR;

namespace LuxTravel.Api.Core.Queries
{
    public class GetAllHotelsQuery : BasePagingRequestDto, IRequest<IEnumerable<HotelDto>>
    {
        public Guid? CityId { get; set; }
        public int GuestCount { get; set; }
        public Guid[] RoomTypeIds { get; set; }
        public int Rating { get; set; }

    }
}
