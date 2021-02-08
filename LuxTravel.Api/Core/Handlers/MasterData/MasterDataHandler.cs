using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Core.Behaviors;
using LuxTravel.Api.Core.Commands;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Model.BaseRepository;
using LuxTravel.Model.Dtos;
using MediatR;

namespace LuxTravel.Api.Core.Handlers.MasterData
{
    public class MasterDataHandler : RequestHandlerBase, 
                                     IRequestHandler<GetAllCitiesQuery, IEnumerable<SelectedObjectDto>>,
                                     IRequestHandler<GetAllRoomTypesQuery, IEnumerable<RoomTypeDto>>,
                                     IRequestHandler<CreateHotelLocationCommand, bool>

    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        public MasterDataHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        public async Task<IEnumerable<SelectedObjectDto>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {

            var data = await  _unitOfWork.CityRepository.GetMany(r=>r.IsActive);
            return _mapper.Map<IEnumerable<SelectedObjectDto>>(data);

        }

        public async Task<IEnumerable<RoomTypeDto>> Handle(GetAllRoomTypesQuery request, CancellationToken cancellationToken)
        {
            var data = await  _unitOfWork.RoomTypeRepository.GetAll();
            return _mapper.Map<IEnumerable<RoomTypeDto>>(data);
            
        }

        public Task<bool> Handle(CreateHotelLocationCommand request, CancellationToken cancellationToken)
        {
            var entity = new Model.Entities.HotelLocation()
            {
                CityId = request.CityId,
                DistrictId = request.DistrictId,
                WardId = request.WardId
            };
            _unitOfWork.HotelLocationRepository.Insert(entity);
            _unitOfWork.SaveChanges();

            return Task.FromResult(true);
        }
    }
}
