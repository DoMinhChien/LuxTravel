using System;
using System.Collections.Generic;

namespace LuxTravel.Models.Entities
{

    public class Ward
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual ICollection<DistrictWardMapping> DistrictWardMappings { get; set; }

    }
}
