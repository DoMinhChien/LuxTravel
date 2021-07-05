using System.Collections.Generic;
using Model.Models;

namespace Service.Interfaces
{
    public interface ICommonService
    {
        List<SelectedItemModel> GetListCity();
        List<SelectedItemModel> GetListDistrict();
        List<SelectedItemModel> GetListWard();
        List<SelectedItemModel> GetListDistrictByCityId(int cityId);
        List<SelectedItemModel> GetListWardByDistrictId(int districtId);
        IEnumerable<CompareObjectModel> Compare2Objects(object newObject, object oldObject);
    }
}
