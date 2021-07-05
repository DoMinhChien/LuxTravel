using EntityFrameworkExtras.EF6;
using System;
using System.Data;

namespace Model.Models
{
    [StoredProcedure("SP_GetNumOfHotelByLocation")]
    public class SP_GetNumOfHotelByLocation
    {
       
    }

    [StoredProcedure("SP_GetListRoomForHomePage")]
    public class SP_GetListRoomForHomePage
    {
        
    }

    [StoredProcedure("SP_GetOffer")]
    public class SP_GetOffer
    {
        [StoredProcedureParameter(SqlDbType.TinyInt, ParameterName = "LocationId")]
        public int LocationId { get; set; }
        [StoredProcedureParameter(SqlDbType.Date, ParameterName = "CheckIn")]
        public DateTime CheckIn { get; set; }
        [StoredProcedureParameter(SqlDbType.Date, ParameterName = "CheckOut")]
        public DateTime CheckOut { get; set; }

    }

    [StoredProcedure("SP_GetListHotelForAdmin")]
    public class SP_GetListHotelForAdmin
    {

    }
}
