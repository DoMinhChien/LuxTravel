using System;

namespace LuxTravel.Models.Entities
{
    public class DistrictWardMapping
    {
        public Guid Id { get; set; }
        public Guid DistrictId { get; set; }
        public Guid WardId { get; set; }
        public bool IsActive { get; set; }
        public bool IsDeleted { get; set; }
        public virtual District District { get; set; }
        public virtual Ward Ward { get; set; }
    }
}
