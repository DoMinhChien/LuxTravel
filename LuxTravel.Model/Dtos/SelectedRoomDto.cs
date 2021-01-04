using System;
using System.Collections.Generic;
using System.Text;

namespace LuxTravel.Model.Dtos
{
    public class SelectedRoomDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Capacity { get; set; }
    }
}
