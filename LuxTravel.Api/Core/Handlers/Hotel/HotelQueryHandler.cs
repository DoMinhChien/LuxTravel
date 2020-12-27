using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Core;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Model;
using LuxTravel.Model.Dtos;
using LuxTravel.Model.Entities;
using LuxTravel.Model.GenericRepository.Interfaces;
using MediatR;
using System.Linq;
using CommonFunctionality.Helper;
using Microsoft.EntityFrameworkCore;
using LuxTravel.Model.Entites;

namespace LuxTravel.Api.Core.Handlers.Hotel
{
    public class HotelQueryHandler : RequestHandlerBase,
        IRequestHandler<GetAllHotelsQuery, PagedList<HotelDto>>,
        IRequestHandler<GetDetailHotelQuery, HotelDto>

    {
        private readonly IBaseRepository<Model.Entities.Hotel, LuxTravelDBContext> _hotelRepo;
        private readonly IBaseRepository<HotelLocation, LuxTravelDBContext> _hotelLocationRepo;

        public HotelQueryHandler(IServiceProvider serviceProvider,
            IBaseRepository<Model.Entities.Hotel, LuxTravelDBContext> hotelRepo,
            IBaseRepository<HotelLocation, LuxTravelDBContext> hotelLocationRepo) : base(serviceProvider)
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
            var hotels = await _hotelRepo.GetAll();
            var records = hotels.Select(r => new HotelDto()
            {
                Id = r.Id,
                Name =  r.Name,
                Phone =  r.Phone,
                Email = r.Email,
                Url =  r.Url
            }).AsQueryable();
            var pagedList =  PagedList<HotelDto>.ToPagedList(records, request.PageIndex, request.PageSize);
            return pagedList;
        }

        public Task<HotelDto> Handle(GetDetailHotelQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }

        //public async Task<HotelDto> Handle(GetDetailHotelQuery request, CancellationToken cancellationToken)
        //{
        //    var entity = _hotelRepo.GetMany(r => r.Id == request.Id).Include(c=>c.Rooms);

        //    return _mapper.Map<HotelDto>(entity);
        //}
    }
}
