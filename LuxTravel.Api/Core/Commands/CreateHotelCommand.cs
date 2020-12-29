using System;
using LuxTravel.Model.Dtos;
using MediatR;

namespace LuxTravel.Api.Core.Commands
{
    public class CreateHotelCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Phone  { get; set; }
        public string Url { get; set; }
        public HotelLocationDto Location { get; set; }

    }
}
