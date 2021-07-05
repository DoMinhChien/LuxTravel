using Service.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using Core.Constant;
using Core.Extensions;
using Data;
using Data.Infrastructure.Interfaces;
using Data.Repository;
using Model.Models;

namespace Service.Services
{
    public class RoomService : IRoomService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICommonService _commonService;

        private readonly RoomRepository _roomRepository;
        private readonly RoomStatusRepository _roomStatusRepository;
        private readonly RoomTypeRepository _roomTypeRepository;
        private readonly ImageStoreRepository _imageStoreRepository;
        private readonly RateRepository _rateRepository;
        private readonly CommonConstant commonConstant;

        public RoomService(IUnitOfWork unitOfWork, ICommonService commonService)
        {
            _unitOfWork = unitOfWork;
            _commonService = commonService;
            _roomRepository = new RoomRepository(_unitOfWork);
            _roomStatusRepository = new RoomStatusRepository(_unitOfWork);
            _roomTypeRepository = new RoomTypeRepository(_unitOfWork);
            _imageStoreRepository = new ImageStoreRepository(_unitOfWork);
            _rateRepository = new RateRepository(_unitOfWork);
            commonConstant = new CommonConstant();
            
        }

        public List<SelectedItemModel> GetListRoomStatus()
        {
            var room = _roomStatusRepository.GetAll();

            return room.Select(r => new SelectedItemModel() { Value = r.Id, Name = r.Code }).ToList();
        }

        public List<RoomTypeModel> GetListRoomType()
        {
            return _roomTypeRepository.GetAll().MapTo<List<RoomTypeModel>>().ToList();

        }

        private void UpdateRoomRate(Guid roomId,decimal newPrice)
        {
            var oldRate = _rateRepository.GetMany(r => r.Room_Id == roomId).FirstOrDefault();
            if (oldRate != null)
            {
                _rateRepository.SoftDelete(oldRate);
            }
            var newEntity = new Rate();
            newEntity.Room_Id = roomId;
            newEntity.Rate1 = newPrice;
            _rateRepository.Insert(newEntity);


        }
        public bool UpdateRoom(RoomModel model)
        {
            if (model.Id != Guid.Empty)
            {
                var room = _roomRepository.GetById(model.Id);

                //room = model.MapTo<Room>();
                room.Name = model.Name;
                room.Room_Type_Id = model.RoomTypeId;
                room.Room_Floor = model.RoomFloor;
                room.Number = model.Number;
                room.Status_Id = model.StatusId;
                //room.Price = model.Price;
                UpdateRoomRate(model.Id, model.Price);
                _roomRepository.Update(room);
            }
            else
            {
                var roomEntity = new Room();

                roomEntity = model.MapTo<Room>();
                roomEntity.Id = Guid.NewGuid();
                //if (model.Images.Any())
                //{
                //    foreach (var img in model.Images)
                //    {
                //        var entiy = new ImageStore();
                //        entiy.objectId = roomEntity.Id;
                //        entiy.ImageByte = img.ImageByte;
                //        entiy.ImagePath = commonConstant.filePath + img.ImageName;
                //        entiy.ImageName = img.ImageName;

                //        _imageStoreRepository.Insert(entiy);
                //    }
                //}
                UpdateRoomRate(roomEntity.Id, model.Price);



                _roomRepository.Insert(roomEntity);
            }

            _unitOfWork.SaveChanges();
            return true;
        }

        public bool DeleteRoom(Guid id)
        {
            var entity = _roomRepository.GetById(id);

            _roomRepository.SoftDelete(entity);

            _unitOfWork.SaveChanges();
            return true;
        }

        public List<ImageStoreModel> GetListImageByRoomId(Guid roomId)
        {
            var imgList = _imageStoreRepository.GetMany(r => r.objectId == roomId).ToList();
            
            var models  = imgList.MapTo<List<ImageStoreModel>>();
            foreach (var img in models)
            {

                img.ImageBase64Str = "data:image/jpg;base64," + Convert.ToBase64String(File.ReadAllBytes(Path.Combine(HttpContext.Current.Server.MapPath(img.ImagePath))));
            }

            return models;
        }

        public List<string> GetListImageName(Guid roomId)
        {
            var imgList = _imageStoreRepository.GetMany(r => r.objectId == roomId).ToList();
            return imgList.Select(r => r.ImageName).ToList();
        }
        public RoomModel GetRoomDetail(Guid id)
        {
            var entity = _roomRepository.GetById(id).MapTo<RoomModel>();
            entity.ListImageName = GetListImageName(id);
            //entity.Images = GetListImageByRoomId(id);
            return entity;
        }

        

        public bool UploadImage(ImageStoreModel model,string filePath)
        {

            var file = model.ImageFile;
            ImageStore img = new ImageStore();

            img.ImageName = file.FileName;
            img.ImageByte = model.ImageByte;
            img.ImagePath = commonConstant.filePath + img.ImageName;
            img.IsDeleted = false;
            img.objectId = model.ObjectId;

            _imageStoreRepository.Insert(img);
            _unitOfWork.SaveChanges();
            return true;
        }
    }


}
