using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Core;
using LuxTravel.Api.Core.Commands;
using LuxTravel.Models.Entities;
using LuxTravel.Models.GenericRepository.Interfaces;
using MediatR;

namespace LuxTravel.Api.Core.Handlers.Booking
{
    public class BookingCommandHandler : RequestHandlerBase, IRequestHandler<CreateBookingCommand, bool>
    {
        private readonly IBaseRepository<Models.Entities.Booking, LuxTravelContext> _bookingRepository;
        private readonly IBaseRepository<BookingDetail, LuxTravelContext> _bookingDetailRepository;
        public BookingCommandHandler(IServiceProvider serviceProvider,
            IBaseRepository<Models.Entities.Booking, LuxTravelContext> bookingRepository,
            IBaseRepository<BookingDetail, LuxTravelContext> bookingDetailRepository) : base(serviceProvider)
        {
            _bookingRepository = bookingRepository;
            _bookingDetailRepository = bookingDetailRepository;
        }
        public Task<bool> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var bookingEntity = new Models.Entities.Booking()
            {
                HotelId = request.HotelId
            };
            throw new NotImplementedException();
        }
    }
}
