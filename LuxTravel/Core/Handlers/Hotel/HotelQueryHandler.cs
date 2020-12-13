using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Core;
using  CommonFunctionality.Helper;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Models;
using LuxTravel.Models.Dtos;
using LuxTravel.Models.Entities;
using LuxTravel.Models.GenericRepository.Interfaces;
using MediatR;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace LuxTravel.Api.Core.Handlers.Hotel
{
    public class HotelQueryHandler : RequestHandlerBase,
        IRequestHandler<GetAllHotelsQuery, PagedList<HotelDto>>,
        IRequestHandler<GetDetailHotelQuery, HotelDto>

    {
        private readonly IBaseRepository<Models.Entities.Hotel, LuxTravelContext> _hotelRepo;
        private readonly IBaseRepository<HotelLocation, LuxTravelContext> _hotelLocationRepo;

        public HotelQueryHandler(IServiceProvider serviceProvider,
            IBaseRepository<Models.Entities.Hotel, LuxTravelContext> hotelRepo,
            IBaseRepository<HotelLocation, LuxTravelContext> hotelLocationRepo) : base(serviceProvider)
        {
            _hotelRepo = hotelRepo;
            _hotelLocationRepo = hotelLocationRepo;
        }


        public async Task<PagedList<HotelDto>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
        {
            //get location 
            var locations = _hotelLocationRepo.GetMany(r => r.CityId == request.CityId);
            var locationIds = locations.Select(r => r.Id).ToList();
            //get all Hotel belong to that location
            //var data = _hotelRepo.GetMany(r => locationIds.Contains(r.HotelLocationId.Value));
            var data = await _hotelRepo.GetAll();
            var result = PagedList<Models.Entities.Hotel>.ToPagedList(data.AsQueryable(), request.PageIndex, request.PageSize);


            return _mapper.Map<PagedList<HotelDto>>(result);
        }

        public async Task<HotelDto> Handle(GetDetailHotelQuery request, CancellationToken cancellationToken)
        {
            var entity = _hotelRepo.GetMany(r => r.Id == request.Id).Include(c=>c.Rooms);

            return _mapper.Map<HotelDto>(entity);
        }
    }
}
