using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LuxTravel.Model.Entites.StoreProcedures
{
    public class SPGetRoomByHotel
    {   
        [Key]
        public Guid RoomId { get; set; }
        public string Name { get; set; }
        public Decimal Price { get; set; }
        public int Capacity { get; set; }
        public int Bed { get; set; }
        public string RoomType { get; set; }
    }
}
