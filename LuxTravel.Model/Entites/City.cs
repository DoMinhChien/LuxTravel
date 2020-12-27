using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LuxTravel.Model.Entities
{
    public class City
    {

        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<CityDistrictMapping> CityDistrictMappings { get; set; }
        public virtual ICollection<HotelLocation> HotelLocations { get; set; }

    }
}
