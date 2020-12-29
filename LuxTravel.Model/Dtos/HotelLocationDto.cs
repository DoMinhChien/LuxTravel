using System;

namespace LuxTravel.Model.Dtos
{
    public class HotelLocationDto
    {
        public Guid CityId { get; set; }
        public Guid DistrictId { get; set; }
        public Guid WardId { get; set; }

    }
}
