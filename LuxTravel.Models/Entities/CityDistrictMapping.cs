using System;
using System.Collections.Generic;
using System.Text;

namespace LuxTravel.Models.Entities
{
    public class CityDistrictMapping
    {
        public Guid Id { get; set; }
        public Guid CityId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public Guid DistrictId { get; set; }
        public virtual City City { get; set; }
        public virtual District District { get; set; }
    }
}
