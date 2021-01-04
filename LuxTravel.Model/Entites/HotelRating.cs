using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using LuxTravel.Model.Entities;

namespace LuxTravel.Model.Entites
{
    public class HotelRating
    {

        [Key, Column(Order = 1)]

        public Guid HotelId { get; set; }
        [Key, Column(Order = 2)]

        public Guid RatorId { get; set; }
        public Decimal Point { get; set; }
        public DateTime CreatedOn { get; set; }
        [ForeignKey("HotelId")]
        public virtual Hotel Hotel { get; set; }
        [ForeignKey("RatorId")]
        public virtual Guest Rator { get; set; }
    }
}
