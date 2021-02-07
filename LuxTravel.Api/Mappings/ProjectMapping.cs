using System;
using AutoMapper;
using LuxTravel.Api.Core.Commands;
using LuxTravel.Api.Core.Queries;
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
            CreateMap<CreateBookingCommand, BookingCalculationDto>();
            CreateMap<GetBookingDetailQuery, BookingCalculationDto>();

        }

        private void CreateMapForEntity()
        {
            CreateMap<CreateHotelCommand, Hotel>();
            CreateMap<CreateBookingCommand, Booking>();
            CreateMap<CreateRoomCommand, Room>();
            CreateMap<HotelDto, Hotel>();
            CreateMap<HotelLocationDto, HotelLocation>().ForMember(dst=>dst.Id, s=>s.MapFrom( src=> Guid.NewGuid()));

        }
    }
}
