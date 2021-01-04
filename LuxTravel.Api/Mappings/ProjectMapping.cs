using System;
using AutoMapper;
using LuxTravel.Api.Core.Commands;
using LuxTravel.Model.Dtos;
using LuxTravel.Model.Entites.StoreProcedures;
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
            CreateMap<RoomType, RoomTypeDto>();

            CreateMap<SPGetRoomByHotel, AvailableRoomDto>();
            CreateMap<SpGetListHotel, HotelDto>();
            CreateMap<SelectedRoomDto, AvailableRoomDto>();
        }

        private void CreateMapForEntity()
        {
            CreateMap<CreateHotelCommand, Hotel>();
            CreateMap<CreateBookingCommand, Booking>();

            CreateMap<HotelDto, Hotel>();
            CreateMap<HotelLocationDto, HotelLocation>().ForMember(dst=>dst.Id, s=>s.MapFrom( src=> Guid.NewGuid()));

        }
    }
}
