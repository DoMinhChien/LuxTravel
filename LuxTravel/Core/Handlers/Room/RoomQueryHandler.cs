using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Core;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Models.Dtos;
using LuxTravel.Models.Entities;
using LuxTravel.Models.GenericRepository.Interfaces;
using MediatR;

namespace LuxTravel.Api.Core.Handlers.Room
{
    public class RoomQueryHandler : RequestHandlerBase,
        IRequestHandler<GetAllRoomByHotelQuery, IEnumerable<RoomDto>>,
        IRequestHandler<GetDetailRoomQuery, RoomDto>

    {
        private readonly IBaseRepository<Models.Entities.Room, LuxTravelContext> _roomRepo;

        public RoomQueryHandler(IServiceProvider serviceProvider,
            IBaseRepository<Models.Entities.Room, LuxTravelContext> roomRepo) : base(serviceProvider)
        {
            _roomRepo = roomRepo;
        }

        public async Task<IEnumerable<RoomDto>> Handle(GetAllRoomByHotelQuery request, CancellationToken cancellationToken)
        {
            var allRooms =  _roomRepo.GetMany(r => r.HotelId == request.HotelId).ToList();
            var result = _mapper.Map<IEnumerable<RoomDto>>(allRooms);
            return result;
        }

        public async Task<RoomDto> Handle(GetDetailRoomQuery request, CancellationToken cancellationToken)
        {
            var entity = await _roomRepo.GetById(request.Id);
            return _mapper.Map<RoomDto>(entity);

        }
    }
}
