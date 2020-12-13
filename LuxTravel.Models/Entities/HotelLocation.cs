using System;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace LuxTravel.Models.Entities
{
    public class HotelLocation
    {
        public Guid Id { get; set; }
        public Guid CityId { get; set; }
        public Guid WardId { get; set; }
        public Guid DistrictId { get; set; }
        public string Address { get; set; }
        public bool IsDeleted { get; set; }
        public Guid? CreatedBy { get; set; }
        public System.DateTime CreatedOn { get; set; }
        public Guid? ModifiedBy { get; set; }
        public DateTime? ModifiedOn { get; set; }

    }
}
