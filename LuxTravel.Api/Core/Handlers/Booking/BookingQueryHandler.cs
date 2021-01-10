using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Core;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Model.BaseRepository;
using LuxTravel.Model.Dtos;
using LuxTravel.Model.Migrations;
using MediatR;
using Microsoft.EntityFrameworkCore;


namespace LuxTravel.Api.Core.Handlers.Booking
{
    public class BookingQueryHandler : RequestHandlerBase,
        IRequestHandler<GetBookingDetailQuery, BookingDto>
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public BookingQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        public Task<BookingDto> Handle(GetBookingDetailQuery request, CancellationToken cancellationToken)
        {
            if (request.RoomId != Guid.Empty)
            {
                var rooms = _unitOfWork.Context.SpGetRoomByHotels
                    .FromSqlInterpolated($"GetRoomByHotelId {request.HotelId} ").ToList();

                var mappingRooms = _mapper.Map<List<AvailableRoomDto>>(rooms);
                var selectedRoom = mappingRooms.FirstOrDefault(r => r.RoomId == request.RoomId);

                var hotel = _unitOfWork.HotelRepository.GetById(request.HotelId);

                //Get room quantity base on Guest count
                int roomCount = request.GuestCount / selectedRoom.Capacity;
                var diffNightTimeSpan = request.DateTo.Subtract(request.DateFrom);  
                var nightCount =(int)diffNightTimeSpan.TotalDays;
                var totals = nightCount * roomCount;
                 var result = new BookingDto()
                 {
                     HotelId =  request.HotelId,
                     HotelName =  hotel.Name,
                     DateFrom = request.DateFrom,
                     DateTo =  request.DateTo,
                     NightCount =  nightCount,
                     RoomCount = roomCount,
                     GuestCount = request.GuestCount,
                     SelectedRoom = _mapper.Map<AvailableRoomDto>(selectedRoom),
                     Totals =  totals * selectedRoom.Price
                 };
                 return Task.FromResult(result);


            }

            return null;
        }



    }
}
