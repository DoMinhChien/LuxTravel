using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Core;
using CommonFunctionality.Core.Behaviors;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Model.Dtos;
using LuxTravel.Model.Entites;
using MediatR;

namespace LuxTravel.Api.Core.Handlers.Room
{
    public class RoomQueryHandler : RequestHandlerBase,
        IRequestHandler<GetAllRoomByHotelQuery, IEnumerable<RoomDto>>,
        IRequestHandler<GetDetailRoomQuery, RoomDto>

    {
        public RoomQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        //public async Task<IEnumerable<RoomDto>> Handle(GetAllRoomByHotelQuery request, CancellationToken cancellationToken)
        //{
        //    var allRooms =  _roomRepo.GetMany(r => r.HotelId == request.HotelId).ToList();
        //    var result = _mapper.Map<IEnumerable<RoomDto>>(allRooms);
        //    return result;
        //}

        public  Task<RoomDto> Handle(GetDetailRoomQuery request, CancellationToken cancellationToken)
        {
            //var entity = await _roomRepo.GetById(request.Id);
            //return _mapper.Map<RoomDto>(entity);
            throw new NotImplementedException();


        }

        public Task<IEnumerable<RoomDto>> Handle(GetAllRoomByHotelQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
