using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Core;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Model.BaseRepository;
using LuxTravel.Model.Dtos;
using MediatR;

namespace LuxTravel.Api.Core.Handlers.MasterData
{
    public class MasterDataHandler : RequestHandlerBase, IRequestHandler<GetAllCitiesQuery, IEnumerable<SelectedObjectDto>>
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
 
    }
}
