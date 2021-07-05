using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Data;
using Model.Models;

namespace Mvc.Infaststructure
{
    public class AutoMapperConfig : AutoMapper.Profile
    {
        // Config automatically mapping between 2 objects
        public AutoMapperConfig()
        {
            ConfigureMapperforEntity();
            ConfigureMapperforModel();
            ConfigureMapperforInput();
            ConfigureMapperforOutput();
        }
        private void ConfigureMapperforEntity()
        {
            CreateMap<HotelModel, Hotel>()
                .ForMember(dst => dst.HotelLocation, s => s.Ignore());
            //.ForMember(dst=>dst.HotelLocation.IsDeleted,s=>s.Ignore())
            //.ForMember(dst => dst.HotelLocation.Ward, s => s.Ignore())
            //.ForMember(dst => dst.HotelLocation.District, s => s.Ignore())
            //.ForMember(dst => dst.HotelLocation.City, s => s.Ignore());
            CreateMap<RoomModel, Room>().ForMember(dst => dst.Id, s => s.MapFrom(src => Guid.NewGuid()));
            //CreateMap<EmployeeModel, Employee>();

            //CreateMap<CategoryModel, Category>()
            //                         .ForMember(dst => dst.Products, s => s.MapFrom(src => src.Products))
            //                         .ForMember(dst => dst.CreatedBy, s => s.MapFrom(src => Guid.Parse("6E2B9DE4-B456-4263-A0F7-CE0432556726")))
            //                         .ForMember(dst => dst.CreatedOn, s => s.MapFrom(src => DateTime.Now));
            CreateMap<ImageStoreModel, ImageStore>();
            CreateMap<BookingModel, Booking>();
            CreateMap<RoomBookedModel, Room_Booked>();
        }


        private void ConfigureMapperforModel()
        {

            CreateMap<Ward, SelectedItemModel>()
                .ForMember(dst => dst.Name, s => s.MapFrom(src => src.WardName))
                .ForMember(dst => dst.Value, s => s.MapFrom(src => src.Id)); ;

            CreateMap<City, SelectedItemModel>()
                .ForMember(dst => dst.Name, s => s.MapFrom(src => src.CityName))
                .ForMember(dst => dst.Value, s => s.MapFrom(src => src.Id)); ;
            CreateMap<District, SelectedItemModel>()
                .ForMember(dst => dst.Name, s => s.MapFrom(src => src.DistrictName))
                .ForMember(dst => dst.Value, s => s.MapFrom(src => src.Id)); ;
            CreateMap<Hotel, HotelModel>();
            CreateMap<Rate, RateModel>()
                .ForMember(dst=>dst.RateTypeModel ,s=>s.MapFrom(src=>src.Rate_Types))
                .ForMember(dst => dst.Rate, s => s.MapFrom(src => src.Rate1));
            CreateMap<Rate_Types, RateTypeModel>();

            CreateMap<ImageStore, ImageStoreModel>().ForMember(dst=>dst.ImageByte,s=>s.Ignore());
            CreateMap<Room_Types, RoomTypeModel>();
            CreateMap<Room, RoomModel>()
                .ForMember(dst => dst.RoomFloor, s => s.MapFrom(src => src.Room_Floor))
                .ForMember(dst => dst.RoomTypeId, s => s.MapFrom(src => src.Room_Type_Id))
                .ForMember(dst => dst.StatusId, s => s.MapFrom(src => src.Status_Id))
                .ForMember(dst => dst.Images, s => s.Ignore());
            CreateMap<Booking, BookingModel>();
            CreateMap<Room, SelectedItemModel>()
                .ForMember(dst=>dst.Value, s=>s.MapFrom(src=>src.Id));
        }
        private void ConfigureMapperforInput()
        {

        }
        private void ConfigureMapperforOutput()
        {


        }
        public static void RegisterMapping()
        {
            AutoMapper.Mapper.Initialize(r => { r.AddProfile<AutoMapperConfig>(); });
        }

    }
    public static class ExtensionMethod
    {

        public static string Encrypt(this Int32 num)
        {

            return "Technotips:" + num;
        }
    }
}