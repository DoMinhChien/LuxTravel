using System;
using System.Collections.Generic;
using System.Text;

namespace LuxTravel.Constants
{
    public enum BookingStatusEnum
    {
        New = 1,
        Inprogress =2,
        Closed =3
    }

    public class BookingStatusMasterData
    {
        public static Dictionary<int, Guid> StatusValue = new Dictionary<int, Guid>()
        {
            { (int)BookingStatusEnum.New, Guid.Parse("262985E5-AB00-408F-A828-E67D158D7E43")},
            { (int)BookingStatusEnum.Inprogress, Guid.Parse("37A7D996-2213-45DF-A6C5-7EA68E0D1A4D")},
            { (int)BookingStatusEnum.Closed, Guid.Parse("B978265A-D5F5-4969-96D4-1128725F1F6A")}
        };
    }
    public enum MasterDataEnum
    {
        City = 1,
        District = 2,
        Ward = 3
    }
}
