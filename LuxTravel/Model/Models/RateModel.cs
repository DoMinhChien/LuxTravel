using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model.Models
{
    public class RateModel
    {
        public Guid Id { get; set; }
        public Guid RoomId { get; set; }
        public Decimal Rate { get; set; }
        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }
        public string Name { get; set; }

        public RateTypeModel RateTypeModel { get; set; }
    }
}
