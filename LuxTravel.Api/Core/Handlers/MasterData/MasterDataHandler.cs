using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Core;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Model.BaseRepository;
using LuxTravel.Model.Dtos;
using MediatR;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace LuxTravel.Api.Core.Handlers.MasterData
{
    public class MasterDataHandler : RequestHandlerBase, 
                                     IRequestHandler<GetAllCitiesQuery, IEnumerable<SelectedObjectDto>>,
                                     IRequestHandler<GetAllRoomTypesQuery, IEnumerable<RoomTypeDto>>
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
    }
}
