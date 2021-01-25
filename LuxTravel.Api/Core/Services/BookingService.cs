using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LuxTravel.Model.BaseRepository;
using LuxTravel.Model.Dtos;
using Microsoft.EntityFrameworkCore;

namespace LuxTravel.Api.Core.Services
{
    public interface IBookingService
    {
        Task<decimal> CalculateTotalMoney(BookingCalculationDto request, AvailableRoomDto selectedRoom);
    }
    public class BookingService : IBookingService
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private readonly IMapper _mapper;

        public BookingService(IMapper mapper)
        {
            _mapper = mapper;
        }

        public Task<decimal> CalculateTotalMoney(BookingCalculationDto request, AvailableRoomDto selectedRoom)
        {

            
            //Get room quantity base on Guest count
            int roomCount = request.GuestCount / selectedRoom.Capacity;
            var diffNightTimeSpan = request.DateTo.Subtract(request.DateFrom);
            var nightCount = (int)diffNightTimeSpan.TotalDays;
            var totals = nightCount * roomCount * selectedRoom.Price;
            
            return Task.FromResult(totals);
        }
    }
}
