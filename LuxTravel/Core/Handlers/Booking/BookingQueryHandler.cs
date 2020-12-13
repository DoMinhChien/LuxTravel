using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Core;
using CommonFunctionality.Helper;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Constants;
using LuxTravel.Models.Dtos;
using LuxTravel.Models.Entities;
using LuxTravel.Models.GenericRepository.Interfaces;
using MediatR;

namespace LuxTravel.Api.Core.Handlers.Booking
{
    public class BookingQueryHandler : RequestHandlerBase,
        IRequestHandler<GetAllBookingsQuery, IEnumerable<BookingDto>>,
        IRequestHandler<GetBookingDetailQuery, BookingDetailDto>
    {
        private readonly IBaseRepository<Models.Entities.Hotel, LuxTravelContext> _hotelRepo;
        private readonly IBaseRepository<HotelLocation, LuxTravelContext> _hotelLocationRepo;
        public BookingQueryHandler(IServiceProvider serviceProvider,
            IBaseRepository<HotelLocation, LuxTravelContext> hotelLocationRepo) : base(serviceProvider)
        {
            _hotelLocationRepo = hotelLocationRepo;
        }
        public async Task<IEnumerable<BookingDto>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
        {
            //Get all hotel belong to location which have respective city
            var listLocation = _hotelLocationRepo.GetMany(r => r.CityId == request.CityId).Select(r => r.Id).ToList(); ;
            var listHotel = _hotelRepo.GetMany(r => listLocation.Contains(r.HotelLocationId.Value)).ToList();
            var listBookings = new List<BookingDto>();
            if (listHotel.Any())
            {
                listHotel.ForEach(r =>
                {
                    listBookings.Add(new BookingDto ()
                    {
                        Id = Guid.NewGuid(),
                        HotelId =  r.Id,
                        HotelName = r.Name,
                        GuestId = GuestId,
                        DateFrom =  request.DateFrom,
                        DateTo =  request.DateTo, 
                        RoomCount =  request.RoomCount,
                        StatusId = BookingStatusMasterData.StatusValue[(int)BookingStatusEnum.New]

                    });
                });

            }

            return listBookings;

        }

        public Task<BookingDetailDto> Handle(GetBookingDetailQuery request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
