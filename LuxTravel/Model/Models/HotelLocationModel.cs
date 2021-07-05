using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
   public class HotelLocationModel
    {
        public Guid Id { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
    }
}
