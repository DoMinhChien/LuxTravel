using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using LuxTravel.Models.Dtos;
using LuxTravel.Models.Entities;

namespace LuxTravel.Api.Mappings
{
    public class ProjectMapping : Profile
    {
        public ProjectMapping()
        {
            CreateMap<City, SelectedObjectDto>();
            CreateMap<Room, RoomDto>();
            CreateMap<Hotel, HotelDto>().ForMember(dst=>dst.Rooms, s=>s.MapFrom( src=>src.Rooms));
        }
    }
}
