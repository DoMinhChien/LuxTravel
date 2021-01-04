using System;
using System.Collections.Generic;
using System.Text;

namespace LuxTravel.Model.Entites
{
    public class Photo
    {
        public Guid Id { get; set; }
        public Guid ObjectId { get; set; }
        public string Url { get; set; }
        public string Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public string PublicId { get; set; }
        public bool IsMain { get; set; }
    }
}
