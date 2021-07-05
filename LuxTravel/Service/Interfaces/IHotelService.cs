using System;
using System.Collections.Generic;
using Model.Models;

namespace Service.Interfaces
{
    public interface IHotelService
    {
        List<HotelModel> GetListHotel();
        bool InsertHotel(HotelModel model);
        bool UpdateHotel(HotelModel model);
        bool DeleteHotel(Guid Id);
        HotelModel GetHotelDetail(Guid HotelId);
        List<SelectedItemModel> GetTopLocation();
        List<HotelModel> GetListHotelForHomepage();
    }
}
