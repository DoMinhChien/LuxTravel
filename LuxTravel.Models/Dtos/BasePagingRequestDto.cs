using System;
using System.Collections.Generic;
using System.Text;

namespace LuxTravel.Models.Dtos
{
    public class BasePagingRequestDto
    {
        public int PageSize { get; set; }
        public int PageIndex { get; set; }
        
    }
}
