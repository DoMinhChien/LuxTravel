using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Model.Models;

namespace Mvc.Inputs
{
    public class HotelInput
    {
        public string Name { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Url { get; set; }
        public int Star_Rating_Id { get; set; }
        public int CityId { get; set; }
        public int DistrictId { get; set; }
        public int WardId { get; set; }
        public Guid LocationId { get; set; }

        public HotelLocationModel HotelLocation;
        
    }
}