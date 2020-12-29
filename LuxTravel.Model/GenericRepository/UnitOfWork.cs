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
