using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LuxTravel.Model.Entites.StoreProcedures
{
    public class SpGetListHotel
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Url { get; set; }
        public string City { get; set; }
        public string  District { get; set; }
        public decimal AvgRating { get; set; }
        public int Reviews { get; set; }
        public decimal SmallestPrice { get; set; }    
    }
}
