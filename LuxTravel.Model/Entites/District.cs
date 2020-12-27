using System;
using System.Collections.Generic;

namespace LuxTravel.Model.Entities
{
    public class District
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<CityDistrictMapping> CityDistrictMappings { get; set; }
        public virtual ICollection<HotelLocation> HotelLocations { get; set; }

        public virtual ICollection<DistrictWardMapping> DistrictWardMappings { get; set; }

    }
}
