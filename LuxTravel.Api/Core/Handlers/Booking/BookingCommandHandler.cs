using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Core;
using LuxTravel.Api.Core.Commands;
using LuxTravel.Api.Core.Services;
using LuxTravel.Constants;
using LuxTravel.Model.BaseRepository;
using LuxTravel.Model.Dtos;
using LuxTravel.Model.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Stripe;

namespace LuxTravel.Api.Core.Handlers.Booking
{
    public class BookingCommandHandler : RequestHandlerBase,
        IRequestHandler<CreateBookingCommand, PaymentConfirmDto>,
        IRequestHandler<ConfirmBookingCommand, bool>
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private readonly IPaymentService _paymentService;
        private readonly IConfiguration _configuration;
        private readonly IBookingService _bookingService;
        private readonly IHttpContextAccessor _httpContextAccessor;
    

        public BookingCommandHandler(IServiceProvider serviceProvider,
            IPaymentService paymentService,
            IConfiguration configuration,
            IBookingService bookingService,
            IHttpContextAccessor httpContextAccessor) : base(serviceProvider)
        {
            _configuration = configuration;
            _paymentService = paymentService;
            _bookingService = bookingService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<PaymentConfirmDto> Handle(CreateBookingCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirstValue(ClaimTypes.NameIdentifier);

            var booking = new Model.Entities.Booking()
            {
                Id = Guid.NewGuid(),
                HotelId = request.HotelId,
                GuestId = Guid.Parse(userId),
                DateFrom = request.DateFrom,
                DateTo = request.DateTo,
                RoomCount = request.RoomCount,
                BookingStatusId = BookingStatusMasterData.StatusValue[(int)BookingStatusEnum.Inprogress]

            };

            var bookingDetail = new BookingDetail()
            {
                BookingId = booking.Id,
                IsActive = true,
                Price = request.SelectedRoom.Price,
                RoomId = request.SelectedRoom.Id

            };
            var input = _mapper.Map<BookingCalculationDto>(request);
            var selectedRoom = _mapper.Map<AvailableRoomDto>(request.SelectedRoom);
            var totals = await _bookingService.CalculateTotalMoney(input, selectedRoom);
            var result = await _paymentService.CreatePaymentIntent(totals);
            result.BookingId = booking.Id;
            _unitOfWork.BookingRepository.Insert(booking);
            _unitOfWork.BookingDetailRepository.Insert(bookingDetail);
            _unitOfWork.SaveChanges();
            return result;
        }

        public Task<bool> Handle(ConfirmBookingCommand request, CancellationToken cancellationToken)
        {
            StripeConfiguration.ApiKey = _configuration.GetSection("StripesSettings:Secretkey").Value;


            if (!string.IsNullOrEmpty(request.Error) || string.IsNullOrEmpty(request.PaymentId))
            {
                //error

                throw new BusinessException("Confirm payment Failed");
            }
            var service = new PaymentIntentService();
            var result = service.Get(request.PaymentId);
            var charges = result.Charges.Data;
            if (charges.Count == 0)
            {
                throw new BusinessException("Confirm payment Failed");
            }

            if (charges.Count == 1)
            {
                var data = charges.FirstOrDefault();
                if (!data.Paid)
                {
                    throw new BusinessException("You not yet pay for this booking");

                }

            }

            var entity = _unitOfWork.BookingRepository.GetById(request.BookingId);

            entity.PaymentId = request.PaymentId;
            _unitOfWork.BookingRepository.Update(entity);

            _unitOfWork.SaveChanges();

            return Task.FromResult<bool>(true);

        }
    }
}
