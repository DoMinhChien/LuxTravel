using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Core;
using LuxTravel.Api.Core.Commands;
using LuxTravel.Model.BaseRepository;
using LuxTravel.Model.Dtos;
using LuxTravel.Model.Entities;
using MediatR;

namespace LuxTravel.Api.Core.Handlers.Hotel
{
    public class HotelCommandHandler : RequestHandlerBase,
        IRequestHandler<CreateHotelCommand, bool>
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public HotelCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        public async Task<bool> Handle(CreateHotelCommand request, CancellationToken cancellationToken)
        {
            var entity = _mapper.Map<Model.Entities.Hotel>(request);
            var locationId = await InsertLocation(request.Location);
            entity.HotelLocationId = locationId;

            _unitOfWork.HotelRepository.Insert(entity);
            _unitOfWork.SaveChanges();
            return true;
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
