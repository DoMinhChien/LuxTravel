using System;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Core;
using LuxTravel.Api.Core.Commands;
using LuxTravel.Model.BaseRepository;
using LuxTravel.Model.Dtos;
using LuxTravel.Model.Entites;
using LuxTravel.Model.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LuxTravel.Api.Core.Handlers.Hotel
{
    public class HotelCommandHandler : RequestHandlerBase,
        IRequestHandler<CreateHotelCommand, bool>,
        IRequestHandler<CreateHotelRatingCommand, bool>
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private readonly IHttpContextAccessor _httpContextAccessor;


        public HotelCommandHandler(IServiceProvider serviceProvider,
            IHttpContextAccessor httpContextAccessor) : base(serviceProvider)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<bool> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            var generator = new RandomGenerator();
        
            var entity = _mapper.Map<Model.Entities.Hotel>(request);
            var locationId = await InsertLocation(request.Location);
            entity.HotelLocationId = locationId;
            entity.Reviewers = generator.RandomNumber(1, 100);
            _unitOfWork.HotelRepository.Insert(entity);
            _unitOfWork.SaveChanges();
            return true;
        }

        public class RandomGenerator
        {
            // Instantiate random number generator.  
            // It is better to keep a single Random instance 
            // and keep using Next on the same instance.  
            private readonly Random _random = new Random();

            // Generates a random number within a range.      
            public int RandomNumber(int min, int max)
            {
                return _random.Next(min, max);
            }
        }

        public Task<bool> Handle(CreateHotelRatingCommand request, CancellationToken cancellationToken)
        {
            var userId = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
            var entity = new HotelRating()
            {
                HotelId = request.HotelId,
                RatorId = new Guid(userId),
                Point = request.Point

            };
            _unitOfWork.HotelRatingRepository.Insert(entity);
            _unitOfWork.SaveChanges();
            return Task.FromResult(true);
        }

        private async Task<Guid> InsertLocation(HotelLocationDto location)
        {

            var existedLocation = await _unitOfWork.HotelLocationRepository.GetMany(r =>
                r.CityId == location.CityId && r.DistrictId == location.DistrictId && r.WardId == location.WardId);
            if (existedLocation != null && existedLocation.Any())
            {
                return existedLocation.FirstOrDefault().Id;
            }

            var newLocation = _mapper.Map<HotelLocation>(location);
            _unitOfWork.HotelLocationRepository.Insert(newLocation);
            return newLocation.Id;
        }
    }
}
