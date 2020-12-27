using System;
using System.Collections.Generic;
using System.Text;

namespace LuxTravel.Model.Entities
{
    public class RoomPrice
    {
        public System.Guid Id { get; set; }
        public System.Guid Room_Id { get; set; }
        public decimal Rate { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public int? CurrencySettingId { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime? CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }


        public virtual CurrencySetting CurrencySetting { get; set; }
        public virtual Room Room { get; set; }
    }
}
