using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Enums
{
    public enum BookingStatus
    {
        [Description("Đã tiếp nhận đơn hàng")]
        OrderAccepted = 1,
        [Description("Đang xử lý đơn hàng")]
        OrderProcessing =2
    }
}
