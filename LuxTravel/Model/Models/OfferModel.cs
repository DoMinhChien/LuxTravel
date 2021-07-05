using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class OfferModel
    {
        public Guid HotelId { get; set; }
        public string   HotelName { get; set; }
        public string CityName { get; set; }
        public string Icon { get; set; }
        public Decimal? MinRate { get; set; }
    }
}

