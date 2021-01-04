using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Core;
using LuxTravel.Api.Core.Commands;
using LuxTravel.Constants;
using LuxTravel.Model.BaseRepository;
using LuxTravel.Model.Entities;
using MediatR;

namespace LuxTravel.Api.Core.Handlers.Booking
{
    public class BookingCommandHandler : RequestHandlerBase, IRequestHandler<CreateBookingCommand, bool>
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public BookingCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }

        public Task<bool> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var userId = new Guid("3E5D8389-523A-435D-B79F-4549368445E2");
            var booking = new Model.Entities.Booking()
            {
                Id =  Guid.NewGuid(),
                HotelId =  request.HotelId,
                GuestId = userId,
                DateFrom =  request.DateFrom,
                DateTo =  request.DateTo,
                RoomCount = request.RoomCount,
                BookingStatusId = BookingStatusMasterData.StatusValue[(int)BookingStatusEnum.Inprogress]

            };

            var bookingDetail = new BookingDetail()
            {
                BookingId = booking.Id,
                IsActive =  true,
                Price = request.SelectedRoom.Price,
                RoomId =  request.SelectedRoom.Id

            };


            _unitOfWork.BookingRepository.Insert(booking);
            _unitOfWork.BookingDetailRepository.Insert(bookingDetail);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
