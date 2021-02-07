using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Core;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Model.Dtos;
using MediatR;
using LuxTravel.Model.BaseRepository;
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


        public Task<IEnumerable<HotelDto>> Handle(GetAllHotelsQuery request, CancellationToken cancellationToken)
        {
            string listRoomTypes = string.Empty;
            if (request.RoomTypeIds != null && request.RoomTypeIds.Any())
            {
                listRoomTypes = string.Join(",", request.RoomTypeIds.Select(r => r));
            }

            if (string.IsNullOrEmpty(request.Sort))
            {
                request.Sort = " Name ";
            }

            if (request.PageIndex <1)
            {
                request.PageIndex = 1;
            }

            if (request.PageSize <10)
            {
                request.PageSize = 10;
            }
             var data = _unitOfWork.Context.SpGetListHotel.FromSqlInterpolated($@"EXEC [dbo].[GetListHotel] @CityId = {request.CityId},  @RoomTypeIds={listRoomTypes} , @Rating = {request.Rating}, @GuestCount = {request.GuestCount}, @PriceFrom = {request.PriceFrom}, @PriceTo = {request.PriceTo}, @PageIndex = {request.PageIndex}, @PageSize = {request.PageSize}, @Sort = {request.Sort}").ToList();

            var records = _mapper.Map<IEnumerable<HotelDto>>(data);

            return Task.FromResult(records);
        }
        private async Task<List<string>> GetHotelImages(Guid id)
        {
            var images = await _unitOfWork.PhotoRepository.GetMany(r => r.ObjectId == id);
            var imageUrls = new List<string>();
            if (images != null && images.Any())
            {
                foreach (var image in images)
                {
                    imageUrls.Add(image.Url);
                }
            }

            return imageUrls;
        }
        private async Task<List<AvailableRoomDto>> GetRoomForHotel(Guid hotelId)
        {
            var rooms = _unitOfWork.Context.SpGetRoomByHotels
                .FromSqlInterpolated($"GetRoomByHotelId {hotelId} ").ToList();
            var result = _mapper.Map<List<AvailableRoomDto>>(rooms);
            if (result.Count >0)
            {
                //Get image for room
                var roomIds = result.Select(r => r.RoomId).ToList();
                var photos = await _unitOfWork.PhotoRepository.GetMany(r => roomIds.Contains(r.ObjectId));
                result.ForEach(r =>
                {
                    var pics = photos.Where(p => p.ObjectId == r.RoomId).ToList();
                    r.ImageUrls = pics.Select(c => c.Url).ToList();
                });
            }
            return result;
        }

        private string GetDetailLocation(Guid locationId)
        {
            var data = _unitOfWork.Context.ViewLocationDetails.FromSqlInterpolated(
                $"SELECT * FROM dbo.GetDetailLocation WHERE LocationId = {locationId}").FirstOrDefault();
            if (data == null)
            {
                return string.Empty;
            }

            return $" {data.WardName}, {data.DistrictName}, {data.CityName}";
        }
        public async Task<HotelDetailDto> Handle(GetDetailHotelQuery request, CancellationToken cancellationToken)
        {
            //Get all hotel belong to location which have respective city
            var selectedHotel = await _unitOfWork.HotelRepository.GetByIdAsync(request.Id);
            if (selectedHotel != null)
            {
                //Get rooms
                var rooms = await GetRoomForHotel(selectedHotel.Id);
                //Get Images for hotel
                var imageUrls = await GetHotelImages(selectedHotel.Id);
                // Rating
                var ratings = await _unitOfWork.HotelRatingRepository.GetMany(r => r.HotelId == selectedHotel.Id);
                var avgRating = ratings.Average(r => r.Point);
                var result =
                    new HotelDetailDto()
                    {
                        Id = selectedHotel.Id,
                        DateFrom = request.DateFrom,
                        DateTo = request.DateTo,
                        GuestId = GuestId,
                        GuestCount = request.GuestCount,
                        HotelName = selectedHotel.Name,
                        AvailableRooms = rooms,
                        ImageUrls = imageUrls,
                        Url = selectedHotel.Url,
                        Email = selectedHotel.Email,
                        AvgRating = avgRating,
                        Location = selectedHotel.HotelLocationId.HasValue ? GetDetailLocation(selectedHotel.HotelLocationId.Value) : string.Empty,
                        EmbedUrl = selectedHotel.EmbedUrl,
                        Reviewers =  selectedHotel.Reviewers
                    };

                return result;
            }
            return new HotelDetailDto();
        }
    }
}
