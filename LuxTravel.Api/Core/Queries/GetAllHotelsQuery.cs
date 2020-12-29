using System;
using CommonFunctionality.Helper;
using LuxTravel.Model.Dtos;
using MediatR;

namespace LuxTravel.Api.Core.Queries
{
    public class GetAllHotelsQuery : BasePagingRequestDto, IRequest<PagedList<HotelDto>>
    {
        public Guid? CityId { get; set; }

    }
}
