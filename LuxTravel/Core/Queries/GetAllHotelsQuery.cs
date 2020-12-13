using System;
using System.Collections.Generic;
using CommonFunctionality.Core;
using CommonFunctionality.Helper;
using LuxTravel.Models.Dtos;
using MediatR;

namespace LuxTravel.Api.Core.Queries
{
    public class GetAllHotelsQuery : BasePagingRequestDto, IRequest<PagedList<HotelDto>>
    {
        public Guid CityId { get; set; }

    }
}
