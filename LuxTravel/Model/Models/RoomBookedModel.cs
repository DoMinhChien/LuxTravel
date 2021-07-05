using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class RoomBookedModel
    {
        public Guid Id { get; set; }
        public Guid BookingId { get; set; }
        public Guid RoomId { get; set; }
        public Decimal Rate { get; set; }
    }   
}
