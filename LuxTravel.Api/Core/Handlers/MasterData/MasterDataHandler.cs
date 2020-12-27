using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Core;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Model.Dtos;
using LuxTravel.Model.Entites;
using LuxTravel.Model.Entities;
using LuxTravel.Model.GenericRepository.Interfaces;
using MediatR;

namespace LuxTravel.Api.Core.Handlers.MasterData
{
    public class MasterDataHandler : RequestHandlerBase, IRequestHandler<GetAllCitiesQuery, IEnumerable<SelectedObjectDto>>
    {
        private readonly IBaseRepository<City, LuxTravelDBContext> _cityRepository;
        public MasterDataHandler(IServiceProvider serviceProvider,
            IBaseRepository<City, LuxTravelDBContext> cityRepository) : base(serviceProvider)
        {

            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<SelectedObjectDto>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            var data = await _cityRepository.GetAll();
            _cityRepository.Dispose();
            return _mapper.Map<IEnumerable<SelectedObjectDto>>(data);

        }

    }
}
