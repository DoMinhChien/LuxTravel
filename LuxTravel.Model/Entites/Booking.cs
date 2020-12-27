using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LuxTravel.Model.Entities
{
    public class Booking
    {
        public Guid Id { get; set; }
        public Guid HotelId { get; set; }
        public Guid GuestId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int RoomCount { get; set; }
        public Guid BookingStatusId { get; set; }
        public Guid? CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

        public virtual BookingStatus BookingStatus { get; set; }
        public virtual Guest Guest { get; set; }
        public virtual Hotel Hotel { get; set; }
        public virtual ICollection<BookingDetail> BookingDetails { get; set; }
        

    }
}
