using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace LuxTravel.Model.Entities
{
    public class RoomType
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal? StandardPrice { get; set; }
        public bool IsDeleted { get; set; }
        public int Beds { get; set; }
        public int Capacity { get; set; }
        public virtual ICollection<Room> Rooms { get; set; }

    }
}
