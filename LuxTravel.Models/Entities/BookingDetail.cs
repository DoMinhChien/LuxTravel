using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LuxTravel.Models.Entities
{
    public class BookingDetail
    {
        [Key, Column(Order = 1)]

        public Guid BookingId { get; set; }
        [Key, Column(Order =2)]

        public Guid RoomId { get; set; }
        public Decimal Price { get; set; }
        public bool IsActive { get; set; }
        [ForeignKey("BookingId")]
        public virtual Booking  Booking { get; set; }
        [ForeignKey("RoomId")]

        public virtual Room Room { get; set; }
    }
}
