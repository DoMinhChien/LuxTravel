using System;
using System.Collections.Generic;
using LuxTravel.Model.Entites;

namespace LuxTravel.Model.Entities
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
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public string Email { get; set; }
        public bool Male { get; set; }

        public virtual ICollection<Booking> Bookings { get; set; }
        public virtual ICollection<HotelRating> Ratings { get; set; }

    }
}
