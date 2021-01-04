using System;
using System.Collections.Generic;
using System.Text;

namespace LuxTravel.Model.Dtos
{
    public class RoomTypeDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Decimal StandardPrice { get; set; }
        public int Beds { get; set; }
        public int Capacity { get; set; }
    }
}
