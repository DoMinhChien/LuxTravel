using System;
using System.Collections.Generic;
using System.Text;

namespace LuxTravel.Model.Entities
{
    public class RoomStatus
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }

    }
}
