using System;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Core;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Model.BaseRepository;
using LuxTravel.Model.Dtos;
using LuxTravel.Model.Migrations;
using MediatR;


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
            if (request.SelectedRoom !=null)
            {
                //Get room quantity base on Guest count
                int roomCount = request.GuestCount / request.SelectedRoom.Capacity;
                var diffNightTimeSpan = request.DateTo.Subtract(request.DateFrom);  
                var nightCount =(int)diffNightTimeSpan.TotalDays;
                var totals = nightCount * roomCount;
                 var result = new BookingDto()
                 {
                     HotelId =  request.HotelId,
                     DateFrom = request.DateFrom,
                     DateTo =  request.DateTo,
                     NightCount =  nightCount,
                     RoomCount = roomCount,
                     GuestCount = request.GuestCount,
                     SelectedRoom = _mapper.Map<AvailableRoomDto>(request.SelectedRoom),
                     Totals =  totals * request.SelectedRoom.Price
                 };
                 return Task.FromResult(result);


            }

            return null;
        }



    }
}
