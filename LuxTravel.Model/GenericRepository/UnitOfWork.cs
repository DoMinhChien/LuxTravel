using System;
using LuxTravel.Model.Entites;
using LuxTravel.Model.Entities;
using LuxTravel.Model.GenericRepository;

namespace LuxTravel.Model.BaseRepository
{
    public class UnitOfWork : IDisposable
    {
        private LuxTravelDBContext context = new LuxTravelDBContext();
        private BaseRepository<City> cityRepository;
        private BaseRepository<Hotel> hotelRepository;
        private BaseRepository<HotelLocation> hotelLocationRepository;
        private BaseRepository<Booking> bookingRepository;
        private BaseRepository<BookingDetail> bookingDetailRepository;
        private BaseRepository<Room> roomRepository;
        private BaseRepository<CurrencySetting> currencySettingRepository;
        private BaseRepository<RoomType> roomTypeRepository;
        private BaseRepository<Guest> guestRepository;
        private BaseRepository<Photo> photoRepository;

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
        public BaseRepository<City> CityRepository
        {
            get
            {

                if (this.cityRepository == null)
                {
                    this.cityRepository = new BaseRepository<City>(context);
                }
                return cityRepository;
            }
        }
        public BaseRepository<Hotel> HotelRepository
        {
            get
            {

                if (this.hotelRepository == null)
                {
                    this.hotelRepository = new BaseRepository<Hotel>(context);
                }
                return hotelRepository;
            }
        }
        public BaseRepository<HotelLocation> HotelLocationRepository
        {
            get
            {

                if (this.hotelLocationRepository == null)
                {
                    this.hotelLocationRepository = new BaseRepository<HotelLocation>(context);
                }
                return hotelLocationRepository;
            }
        }


        public BaseRepository<Booking> BookingRepository
        {
            get
            {

                if (this.bookingRepository == null)
                {
                    this.bookingRepository = new BaseRepository<Booking>(context);
                }
                return bookingRepository;
            }
        }
        public BaseRepository<BookingDetail> BookingDetailRepository
        {
            get
            {

                if (this.bookingDetailRepository == null)
                {
                    this.bookingDetailRepository = new BaseRepository<BookingDetail>(context);
                }
                return bookingDetailRepository;
            }
        }

        public BaseRepository<Room> RoomRepository
        {
            get
            {

                if (this.roomRepository == null)
                {
                    this.roomRepository = new BaseRepository<Room>(context);
                }
                return roomRepository;
            }
        }

        public BaseRepository<CurrencySetting> CurrencySetting
        {
            get
            {

                if (this.currencySettingRepository == null)
                {
                    this.currencySettingRepository = new BaseRepository<CurrencySetting>(context);
                }
                return currencySettingRepository;
            }
        }
        public BaseRepository<RoomType> RoomTypeRepository
        {
            get
            {

                if (this.roomTypeRepository == null)
                {
                    this.roomTypeRepository = new BaseRepository<RoomType>(context);
                }
                return roomTypeRepository;
            }
        }
        public BaseRepository<Guest> GuestRepository
        {
            get
            {

                if (this.guestRepository == null)
                {
                    this.guestRepository = new BaseRepository<Guest>(context);
                }
                return guestRepository;
            }
        }
        public BaseRepository<Photo> PhotoRepository
        {
            get
            {

                if (this.photoRepository == null)
                {
                    this.photoRepository = new BaseRepository<Photo>(context);
                }
                return photoRepository;
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
