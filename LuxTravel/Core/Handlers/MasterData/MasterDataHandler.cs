using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper.Configuration.Annotations;
using CommonFunctionality.Core;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Models.Dtos;
using LuxTravel.Models.Entities;
using LuxTravel.Models.GenericRepository.Interfaces;
using MediatR;

namespace LuxTravel.Api.Core.Handlers.MasterData
{
    public class MasterDataHandler : RequestHandlerBase, IRequestHandler<GetAllCitiesQuery, IEnumerable<SelectedObjectDto>>
    {
        private readonly IBaseRepository<City, LuxTravelContext> _cityRepository;
        public MasterDataHandler(IServiceProvider serviceProvider,
            IBaseRepository<City, LuxTravelContext> cityRepository) : base(serviceProvider)
        {

            _cityRepository = cityRepository;
        }

        public async Task<IEnumerable<SelectedObjectDto>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
        {
            var data = _cityRepository.GetMany(r=>r.IsActive).ToList();
            return _mapper.Map<IEnumerable<SelectedObjectDto>>(data);
        }
    }
}
