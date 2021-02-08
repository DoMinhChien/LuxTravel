using System;
using LuxTravel.Model.Entites;
using LuxTravel.Model.Entities;
using  CommonFunctionality.Core.GenericRepository;

namespace LuxTravel.Model.BaseRepository
{
    public class UnitOfWork : IDisposable
    {
        public LuxTravelDBContext context = new LuxTravelDBContext();
        private BaseRepository<City, LuxTravelDBContext> cityRepository;
        private BaseRepository<Hotel, LuxTravelDBContext> hotelRepository;
        private BaseRepository<HotelLocation, LuxTravelDBContext> hotelLocationRepository;
        private BaseRepository<Booking, LuxTravelDBContext> bookingRepository;
        private BaseRepository<BookingDetail, LuxTravelDBContext> bookingDetailRepository;
        private BaseRepository<Room, LuxTravelDBContext> roomRepository;
        private BaseRepository<CurrencySetting, LuxTravelDBContext> currencySettingRepository;
        private BaseRepository<RoomType, LuxTravelDBContext> roomTypeRepository;
        private BaseRepository<Guest, LuxTravelDBContext> guestRepository;
        private BaseRepository<Photo, LuxTravelDBContext> photoRepository;
        private BaseRepository<HotelRating, LuxTravelDBContext> hotelRatingRepository;

        public LuxTravelDBContext Context
        {
            get
            {

                if (this.context == null)
                {
                    this.context = new LuxTravelDBContext();
                }
                return context;
            }
        }

        /// <summary>
        /// Implementation
        /// </summary>
        public BaseRepository<City, LuxTravelDBContext> CityRepository
        {
            get
            {

                if (this.cityRepository == null)
                {
                    this.cityRepository = new BaseRepository<City, LuxTravelDBContext>(context);
                }
                return cityRepository;
            }
        }
        public BaseRepository<Hotel, LuxTravelDBContext> HotelRepository
        {
            get
            {

                if (this.hotelRepository == null)
                {
                    this.hotelRepository = new BaseRepository<Hotel, LuxTravelDBContext>(context);
                }
                return hotelRepository;
            }
        }
        public BaseRepository<HotelLocation, LuxTravelDBContext> HotelLocationRepository
        {
            get
            {

                if (this.hotelLocationRepository == null)
                {
                    this.hotelLocationRepository = new BaseRepository<HotelLocation, LuxTravelDBContext>(context);
                }
                return hotelLocationRepository;
            }
        }


        public BaseRepository<Booking, LuxTravelDBContext> BookingRepository
        {
            get
            {

                if (this.bookingRepository == null)
                {
                    this.bookingRepository = new BaseRepository<Booking, LuxTravelDBContext>(context);
                }
                return bookingRepository;
            }
        }
        public BaseRepository<BookingDetail, LuxTravelDBContext> BookingDetailRepository
        {
            get
            {

                if (this.bookingDetailRepository == null)
                {
                    this.bookingDetailRepository = new BaseRepository<BookingDetail, LuxTravelDBContext>(context);
                }
                return bookingDetailRepository;
            }
        }

        public BaseRepository<Room, LuxTravelDBContext> RoomRepository
        {
            get
            {

                if (this.roomRepository == null)
                {
                    this.roomRepository = new BaseRepository<Room, LuxTravelDBContext>(context);
                }
                return roomRepository;
            }
        }

        public BaseRepository<CurrencySetting, LuxTravelDBContext> CurrencySetting
        {
            get
            {

                if (this.currencySettingRepository == null)
                {
                    this.currencySettingRepository = new BaseRepository<CurrencySetting, LuxTravelDBContext>(context);
                }
                return currencySettingRepository;
            }
        }
        public BaseRepository<RoomType, LuxTravelDBContext> RoomTypeRepository
        {
            get
            {

                if (this.roomTypeRepository == null)
                {
                    this.roomTypeRepository = new BaseRepository<RoomType, LuxTravelDBContext>(context);
                }
                return roomTypeRepository;
            }
        }
        public BaseRepository<Guest, LuxTravelDBContext> GuestRepository
        {
            get
            {

                if (this.guestRepository == null)
                {
                    this.guestRepository = new BaseRepository<Guest, LuxTravelDBContext>(context);
                }
                return guestRepository;
            }
        }
        public BaseRepository<Photo, LuxTravelDBContext> PhotoRepository
        {
            get
            {

                if (this.photoRepository == null)
                {
                    this.photoRepository = new BaseRepository<Photo, LuxTravelDBContext>(context);
                }
                return photoRepository;
            }
        }
        public BaseRepository<HotelRating, LuxTravelDBContext> HotelRatingRepository
        {
            get
            {

                if (this.hotelRatingRepository == null)
                {
                    this.hotelRatingRepository = new BaseRepository<HotelRating, LuxTravelDBContext>(context);
                }
                return hotelRatingRepository;
            }
        }
        public void SaveChanges()
        {
            context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
