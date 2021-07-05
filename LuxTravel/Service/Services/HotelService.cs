using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.IO.Ports;
using System.Linq;
using System.Web.Compilation;
using Core.Extensions;
using Data;
using Data.Infrastructure.Interfaces;
using Data.Repository;
using EntityFrameworkExtras.EF6;
using Model.Models;
using Service.Interfaces;

namespace Service.Services
{

    public class HotelService : IHotelService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommonService _commonService;
        private readonly HotelRepository _hotelRepository;
        private readonly IRoomService _roomService;
        private readonly HotelLocationRepository _hotelLocationRepository;
        private readonly RateRepository _rateRepository;
        private readonly ImageStoreRepository _imageStoreRepository;
        public HotelService(IUnitOfWork unitOfWork,ICommonService commonService, IRoomService roomService)
        {
            _unitOfWork = unitOfWork;
            //_dbContext = dbContext;
            _commonService = commonService;
            _roomService = roomService;
            _hotelRepository = new HotelRepository(_unitOfWork);
            _hotelLocationRepository = new HotelLocationRepository(_unitOfWork);
            _imageStoreRepository = new ImageStoreRepository(_unitOfWork);

            _rateRepository =  new RateRepository(_unitOfWork);
        }

        public List<HotelModel> GetListHotel()
        {
            var data = _unitOfWork.Db.Database.ExecuteStoredProcedure<HotelModel>(new SP_GetListHotelForAdmin());
            return data.ToList();
        }
        public bool InsertHotel(HotelModel model)
        {
            var entity = new Hotel();
            entity = model.MapTo<Hotel>();
            entity.Star_Rating_Id = 1;
            var locationId = InsertLocationForHotel(model.HotelLocation);
            entity.Location_Id = locationId;
            _hotelRepository.Insert(entity);
            
            _unitOfWork.SaveChanges();

            return true;

        }

        private Guid InsertLocationForHotel(HotelLocationModel model)
        {
            var id = Guid.NewGuid();
            _hotelLocationRepository.Insert(new HotelLocation() {Id = id, City_Id = model.CityId, Ward_Id = model.WardId,District_Id = model.DistrictId});
            return id;
        }
        public bool UpdateHotel(HotelModel model)
        {

            var entity = _hotelRepository.GetById(model.Id);

            var oldEntity = entity.MapTo<HotelModel>();

            if (model.HotelLocation.Id != Guid.Empty)
            {
                UpdateLocationForHotel(entity, model);
            }


            
            entity.Name = model.Name;
            entity.Email = model.Email;
            //entity.Rooms.Add(room);
            // _historyBLL.SaveHistory(oldEntity, HotelModel, "Has updated this Hotel");
            _hotelRepository.Update(entity);
            _unitOfWork.SaveChanges();
            return true;

        }

 
        private void UpdateLocationForHotel(Hotel entity, HotelModel model)
        {
            if (model.HotelLocation.WardId != entity.HotelLocation.Ward_Id || model.HotelLocation.DistrictId != entity.HotelLocation.District_Id || model.HotelLocation.CityId != entity.HotelLocation.City_Id)
            {
                var old = entity.HotelLocation.MapTo<HotelLocationModel>();
                var detail = _commonService.Compare2Objects(old, model.HotelLocation);
                if (detail.Any())
                {
                    foreach (var item in detail)
                    {
                        if (item.Field == "CityId")
                        {
                            entity.HotelLocation.City_Id = int.Parse(item.NewValue);

                        }
                        if (item.Field == "DistrictId")
                        {
                            entity.HotelLocation.District_Id = int.Parse(item.NewValue);

                        }
                        if (item.Field == "WardId")
                        {
                            entity.HotelLocation.Ward_Id = int.Parse(item.NewValue);

                        }

                    }
                }


            }
        }
        public bool DeleteHotel(Guid id)
        {
            bool result = false;
            var entity = _hotelRepository.GetById(id);
            if (entity != null)
            {
                result = _hotelRepository.SoftDelete(entity);
            }
            _hotelRepository.Update(entity);
            _unitOfWork.SaveChanges();

            return result;
        }

        public HotelModel GetHotelDetail(Guid hotelId)
        {
           var entity = _hotelRepository.GetById(hotelId);
           entity.Rooms = entity.Rooms.Where(r=>!r.IsDeleted).ToList();


           var model = entity.MapTo<HotelModel>();
           model.ListImageName = GetHotelImageName(hotelId);
 
            return model;
        }

        private List<SelectedItemModel> GetHotelImageName(Guid hotelId)
        {
            var data = _imageStoreRepository.GetMany(r => r.objectId == hotelId).Select(c => new SelectedItemModel
            {
                Value = c.ImagePath,
                Name = c.ImageName
            });
            return data.ToList();
        }
        private List<RateModel> GetRoomRates(Guid roomId)
        {
            return _rateRepository.GetMany(r => r.Room_Id == roomId).Include(r=>r.Rate_Types).MapTo<List<RateModel>>();

        }

        public List<SelectedItemModel> GetTopLocation()
        {

            var obj = new SP_GetNumOfHotelByLocation();
            var data = _unitOfWork.Db.Database.ExecuteStoredProcedure<SelectedItemModel>(obj);
            return data.ToList();

            //return new List<SelectedItemModel>();
        }

        public List<HotelModel> GetListHotelForHomepage()
        {
            var data = _unitOfWork.Db.Database.ExecuteStoredProcedure<HotelModel>(new SP_GetListRoomForHomePage());
            return data.ToList();
        }
    }
}
