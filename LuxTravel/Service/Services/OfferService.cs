using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Enums;
using Core.Extensions;
using Data;
using Data.Infrastructure.Interfaces;
using Data.Repository;
using EntityFrameworkExtras.EF6;
using Model.Models;
using Service.Interfaces;

namespace Service.Services
{
    public class OfferService : IOfferService
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly BookingRepository _bookingRepository;
        private readonly RoomBookedRepository _roomBookedRepository;

        public OfferService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            _bookingRepository = new BookingRepository(_unitOfWork);
            _roomBookedRepository= new RoomBookedRepository(_unitOfWork);
        }

        public bool ProcessOffer(BookingModel offer)
        {
            var bookingEntity = new Booking();
            bookingEntity.Id =Guid.NewGuid();
            var guestId = Guid.Parse("3E5D8389-523A-435D-B79F-4549368445E2");
            bookingEntity = offer.MapTo<Booking>();
            bookingEntity.Guest_Id = guestId;
            bookingEntity.ReservationId = Guid.Parse("0520bc92-60cd-403e-b4e7-4bd7e4ad9002");
            //bookingEntity.Status_Id = (int)BookingStatus.OrderAccepted;

            _bookingRepository.Insert(bookingEntity);
            
            var roomBooked = new Room_Booked();
            roomBooked = offer.RoomBookedModel.MapTo<Room_Booked>();
            roomBooked.Booking_Id = bookingEntity.Id;

            _roomBookedRepository.Insert(roomBooked);
            _unitOfWork.SaveChanges();      
            return true;
        }

        public List<OfferModel> GetOffer(OfferFilterModel filterModel)
        {


            var obj = new SP_GetOffer
            {
                CheckIn = filterModel.DateFrom,
                CheckOut = filterModel.DateTo,
                LocationId = filterModel.LocationId
            };
            var data = _unitOfWork.Db.Database.ExecuteStoredProcedure<OfferModel>(obj);
            return data.ToList();
        }
        
    }
}
