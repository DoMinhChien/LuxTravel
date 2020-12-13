using System;
using System.Collections.Generic;

namespace LuxTravel.Models.Entities
{
    public class Guest
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public int Point { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? CreatedBy { get; set; }
        public DateTime CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }
      //  public virtual ICollection<Booking> Bookings { get; set; }

    }
}
