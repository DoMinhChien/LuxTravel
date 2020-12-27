using AutoMapper;
using LuxTravel.Model.Dtos;
using LuxTravel.Model.Entities;

namespace LuxTravel.Api.Mappings
{
    public class ProjectMapping : Profile
    {
        public ProjectMapping()
        {
            CreateMap<City, SelectedObjectDto>();
            CreateMap<Room, RoomDto>();
            //CreateMap<Hotel, HotelDto>().ForMember(dst => dst.Rooms, s => s.MapFrom(src => src.Rooms));

        }
    }
}
