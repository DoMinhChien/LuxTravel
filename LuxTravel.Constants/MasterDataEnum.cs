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

    public enum RoomTypeEnum
    {
        Single = 1,
        Double = 2,
        Twin = 3
    }
    public class RoomTypeEnumMasterData
    {
        public static Dictionary<int, Guid> StatusValue = new Dictionary<int, Guid>()
        {
            { (int)RoomTypeEnum.Single, Guid.Parse("A72D6BA2-6632-4835-8A46-AA9C6BAA4529")},
            { (int)RoomTypeEnum.Double, Guid.Parse("96FF0888-5C34-4284-BEEE-250C06FBEBE6")},
            { (int)RoomTypeEnum.Twin, Guid.Parse("149B1CF7-DF1F-4EF5-9D0B-5B640358D31D")}
        };
    }
}
