using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Model.Models
{
    public class RoomModel
    {

        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public string Name { get; set; }

        public int Number { get; set; }
        public decimal Price { get; set; }
        public int RoomFloor { get; set; }
        public int RoomTypeId { get; set; }
        public int StatusId { get; set; }

        public List<RateModel> Rates { get; set; }
        public List<ImageStoreModel> Images { get; set; }
        public List<string> ListImageName { get; set; }
    }
}
