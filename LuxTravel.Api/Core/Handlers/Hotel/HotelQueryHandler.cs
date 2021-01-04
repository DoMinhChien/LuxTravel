using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Core;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Model.Dtos;
using MediatR;
using CommonFunctionality.Helper;
using LuxTravel.Model.BaseRepository;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace LuxTravel.Api.Core.Handlers.Hotel
{
    public class HotelQueryHandler : RequestHandlerBase,
        IRequestHandler<GetAllHotelsQuery, IEnumerable<HotelDto>>,
        IRequestHandler<GetDetailHotelQuery, HotelDetailDto>

    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public HotelQueryHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
        }


        public  Task<IEnumerable<HotelDto>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
        {
            string listRoomTypes = string.Empty;
            var parameters = new List<SqlParameter>();
            string query = $"EXEC [dbo].[GetListHotel] @CitytId ='{request.CityId}' ";
            if (request.RoomTypeIds != null && request.RoomTypeIds.Any())
            {
                listRoomTypes = string.Join(",", request.RoomTypeIds.Select(r => r));
                query = query + $", @RoomTypeIds={listRoomTypes} ";

            }
            
            query = query + $", @Rating = {request.Rating}, @GuestCount = {request.GuestCount}";
            var data = _unitOfWork.Context.SpGetListHotel
                .FromSqlInterpolated($"EXEC [dbo].[GetListHotel] @CitytId = {request.CityId}, @RoomTypeIds={listRoomTypes} , @Rating = {request.Rating}, @GuestCount = {request.GuestCount}").ToList();

            var records = _mapper.Map<IEnumerable<HotelDto>>(data);

            return Task.FromResult(records);
        }

        private async Task GetSmallestPrice(Guid hotelId)
        {
            var rooms = await _unitOfWork.RoomRepository.GetMany(r => r.HotelId == hotelId);

            //var smallestPrice = rooms.Min(r=>r.Pr)

        }

        public async Task<HotelDetailDto> Handle(GetDetailHotelQuery request, CancellationToken cancellationToken)
        {
            //Get all hotel belong to location which have respective city
            var selectedHotel = await _unitOfWork.HotelRepository.GetByIdAsync(request.Id);
            if (selectedHotel != null)
            {
                //Get rooms
                var rooms = _unitOfWork.Context.SpGetRoomByHotels
                    .FromSqlInterpolated($"GetRoomByHotelId {selectedHotel.Id} ").ToList();

                var result =
                    new HotelDetailDto()
                    {
                        Id = selectedHotel.Id,
                        DateFrom = request.DateFrom,
                        DateTo = request.DateTo,
                        GuestId = GuestId,
                        GuestCount = request.GuestCount,
                        HotelName = selectedHotel.Name,
                        AvailableRooms = _mapper.Map<List<AvailableRoomDto>>(rooms)
                    };

                return result;
            }
            return new HotelDetailDto();
        }
    }
}
