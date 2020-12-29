using System;
using AutoMapper;
using LuxTravel.Api.Core.Commands;
using LuxTravel.Model.Dtos;
using LuxTravel.Model.Entities;

namespace LuxTravel.Api.Mappings
{
    public class ProjectMapping : Profile
    {
        public ProjectMapping()
        {
            CreateMapForDto();
            CreateMapForEntity();


        }

        private void CreateMapForDto()
        {
            CreateMap<City, SelectedObjectDto>();
            CreateMap<Room, RoomDto>();
            CreateMap<Hotel, HotelDto>();
        }

        private void CreateMapForEntity()
        {
            CreateMap<CreateHotelCommand, Hotel>();

            CreateMap<HotelDto, Hotel>();
            CreateMap<HotelLocationDto, HotelLocation>().ForMember(dst=>dst.Id, s=>s.MapFrom( src=> Guid.NewGuid()));

        }
    }
}
