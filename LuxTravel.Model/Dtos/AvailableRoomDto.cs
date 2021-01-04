using System;
using System.Collections.Generic;
using System.Text;

namespace LuxTravel.Model.Dtos
{
    public class AvailableRoomDto
    {
        public Guid RoomId { get; set; }
        public string Name { get; set; }
        public Decimal Price { get; set; }
        public int Capacity { get; set; }
        public int Bed { get; set; }
        public string RoomType { get; set; }
    }
}
