using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Core;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Constants;
using LuxTravel.Model.BaseRepository;
using LuxTravel.Model.Dtos;
using LuxTravel.Model.Entites;
using LuxTravel.Model.Entities;
using MediatR;

namespace LuxTravel.Api.Core.Handlers.Booking
{
    public class BookingQueryHandler : RequestHandlerBase,
        IRequestHandler<GetAllBookingsQuery, IEnumerable<BookingDto>>,
        IRequestHandler<GetBookingDetailQuery, BookingDetailDto>
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public BookingQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }
        public async Task<IEnumerable<BookingDto>> Handle(GetAllBookingsQuery request, CancellationToken cancellationToken)
        {
            //Get all hotel belong to location which have respective city
            var listLocations = await _unitOfWork.HotelLocationRepository.GetMany(r => r.CityId == request.CityId) ;
            var locationIds = listLocations.Select(r => r.Id).ToList();
            var listHotel = await _unitOfWork.HotelRepository.GetMany(r => locationIds.Contains(r.HotelLocationId.Value));
            var listBookings = new List<BookingDto>();
            if (listHotel.Any())
            {
                listHotel.ToList().ForEach(r =>
                {
                    listBookings.Add(new BookingDto()
                    {
                        Id = Guid.NewGuid(),
                        HotelId = r.Id,
                        HotelName = r.Name,
                        GuestId = GuestId,
                        DateFrom = request.DateFrom,
                        DateTo = request.DateTo,
                        RoomCount = request.RoomCount,
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
