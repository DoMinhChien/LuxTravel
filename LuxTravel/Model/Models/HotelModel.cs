using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
   public class HotelModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Url { get; set; }
        public  Decimal Price { get; set; }

        public HotelLocationModel HotelLocation { get; set; }
        public IEnumerable<RoomModel> Rooms { get; set;}
        
        public string CityName { get; set; }
        public string RatingIcon { get; set; }

        public List<SelectedItemModel> ListImageName { get; set; }
        public DateTime? ModifiedOn { get; set; }
    }
}
