using System;

namespace LuxTravel.Model.Entities
{
   public class CurrencySetting
    {
        public Guid Id { get; set; }
        public string Code { get; set; }
        public string Description { get; set; }
        public bool IsDeleted { get; set; }
        public string Icon { get; set; }
    }
}
