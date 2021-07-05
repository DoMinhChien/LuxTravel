using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using Core.Extensions;
using Data.Infrastructure.Interfaces;
using Data.Repository;
using Model.Models;
using Service.Interfaces;

namespace Service.Services
{
    public class CommonService : CompareObjectExtension<CompareObjectModel>, ICommonService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly CityRepository _cityRepository;
        private readonly DistrictRepository _districtRepository;
        private readonly CityDistrictMappingRepository _cityDistrictMappingRepository;
        private readonly DistrictWardMappingRepository _districtWardMappingRepository;
        private readonly WardRepository _wardRepository;
        public CommonService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            this._cityRepository = new CityRepository(_unitOfWork);
            this._districtRepository = new DistrictRepository(_unitOfWork);
            this._cityDistrictMappingRepository = new CityDistrictMappingRepository(_unitOfWork);
            this._districtWardMappingRepository = new DistrictWardMappingRepository(_unitOfWork);
            this._wardRepository = new WardRepository(_unitOfWork);
        }

        public List<SelectedItemModel> GetListCity()
        {
            var data = _cityRepository.GetAll(r => r.IsActive).ToList();
            return data.MapTo<List<SelectedItemModel>>();
        }
        public List<SelectedItemModel> GetListDistrict()
        {
            var data = _districtRepository.GetAll(r => r.IsActive).ToList();
            return data.MapTo<List<SelectedItemModel>>();
        }

        public List<SelectedItemModel> GetListWard()
        {
            var data = _wardRepository.GetMany(r => !r.IsDeleted).ToList();
            return data.MapTo<List<SelectedItemModel>>();
        }

        public List<SelectedItemModel> GetListDistrictByCityId(int cityId)
        {
            var data = _cityDistrictMappingRepository.GetMany(r => r.City_Id == cityId).Include(r => r.District)
                .ToList().Select(c=> new SelectedItemModel(){Value = c.District_Id,Name = c.District.DistrictName}).ToList();

            return data;

        }
        public List<SelectedItemModel> GetListWardByDistrictId(int districtId)
        {
            var data = _districtWardMappingRepository.GetMany(r => r.District_Id == districtId).Include(r => r.Ward)
                .ToList().Select(c => new SelectedItemModel() { Value = c.Ward_Id, Name = c.Ward.WardName }).ToList();

            return data;

        }

        public IEnumerable<CompareObjectModel> Compare2Objects(object newObject, object oldObject)
        {
            return  CompareObject(newObject, oldObject);
            
        }

     
    }
}
