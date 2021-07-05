using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class OfferFilterModel : BasePagingModel
    {
        public int LocationId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int RoomCount { get; set; }
        public int GuestCount { get; set; }
    }
}
