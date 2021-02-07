using System;
using System.Collections.Generic;
using System.Text;

namespace LuxTravel.Model.Dtos
{
    public class HotelDetailDto
    {
        public HotelDetailDto()
        {
            ImageUrls = new List<string>();
        }
        public Guid Id { get; set; }
        public string HotelName { get; set; }
        public Guid GuestId { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public int GuestCount { get; set; }
        public Guid StatusId { get; set; }
        public List<string> ImageUrls { get; set; }
        public IEnumerable<AvailableRoomDto> AvailableRooms { get; set; }
        public string Url { get; set; }
        public string Email { get; set; }
        public Decimal AvgRating { get; set; }
        public int Reviewers { get; set; }
        public string EmbedUrl { get; set; }
        public string Location { get; set; }
    }
}
