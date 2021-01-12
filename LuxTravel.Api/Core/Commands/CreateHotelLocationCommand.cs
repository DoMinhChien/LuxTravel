using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;

namespace LuxTravel.Api.Core.Commands
{
    public class CreateHotelLocationCommand : IRequest<bool>
    {
        public Guid CityId { get; set; }
        public Guid DistrictId { get; set; }
        public Guid WardId { get; set; }
    }
}
