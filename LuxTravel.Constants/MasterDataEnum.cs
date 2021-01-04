using System;
using System.Collections.Generic;
using System.Text;

namespace LuxTravel.Constants
{
    public enum BookingStatusEnum
    {
       // New = 1,
        Inprogress =1,
        Closed =2
    }

    public class BookingStatusMasterData
    {
        public static Dictionary<int, Guid> StatusValue = new Dictionary<int, Guid>()
        {
           // { (int)BookingStatusEnum.New, Guid.Parse("262985E5-AB00-408F-A828-E67D158D7E43")},
            { (int)BookingStatusEnum.Inprogress, Guid.Parse("2565189D-411C-43CD-BFF1-3F6C2C0D6B27")},
            { (int)BookingStatusEnum.Closed, Guid.Parse("EE3F767E-A225-42DD-A06B-B6F945370EBC")}
        };
    }
    public enum MasterDataEnum
    {
        City = 1,
        District = 2,
        Ward = 3
    }
}
