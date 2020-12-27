using System;
using System.Collections.Generic;
using System.Text;

namespace LuxTravel.Model.Entities
{
    public class BookingStatus
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
