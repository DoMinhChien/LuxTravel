using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class BookingModel
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public Guid GuestId { get; set; }
        public Guid ReservationId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int StatusId { get; set; }
        public int RoomCount { get; set; }
        public Guid RoomId { get; set; }    
        public RoomBookedModel RoomBookedModel { get; set; }
        
    }
}
