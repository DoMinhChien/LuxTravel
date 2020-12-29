using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Core;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Model.Dtos;
using MediatR;
using CommonFunctionality.Helper;
using LuxTravel.Model.BaseRepository;

namespace LuxTravel.Api.Core.Handlers.Hotel
{
    public class HotelQueryHandler : RequestHandlerBase,
        IRequestHandler<GetAllHotelsQuery, PagedList<HotelDto>>,
        IRequestHandler<GetDetailHotelQuery, HotelDto>

    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public HotelQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }


        public async Task<PagedList<HotelDto>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
        {
            List<Guid> locationIds = new List<Guid>();
            IEnumerable<Model.Entities.Hotel> data = null;
            if (request.CityId.HasValue)
            {
                var locations = await _unitOfWork.HotelLocationRepository.GetMany(r => r.CityId == request.CityId);
                locationIds = locations.Select(r => r.Id).ToList();
                data = await _unitOfWork.HotelRepository.GetMany(r => locationIds.Contains(r.HotelLocationId.Value));

            }
            else
            {
                data = await _unitOfWork.HotelRepository.GetAll();
            }


            var records = data.Select(r => new HotelDto()
            {
                Id = r.Id,
                Name = r.Name,
                Phone = r.Phone,
                Email = r.Email,
                Url = r.Url
            }).AsQueryable();
            var pagedList = PagedList<HotelDto>.ToPagedList(records, request.PageIndex, request.PageSize);
            return pagedList;
        }


        public async Task<HotelDto> Handle(GetDetailHotelQuery request, CancellationToken cancellationToken)
        {
            var entity = await _unitOfWork.HotelRepository.GetByIdAsync(request.Id);

            return _mapper.Map<HotelDto>(entity);
        }
    }
}
