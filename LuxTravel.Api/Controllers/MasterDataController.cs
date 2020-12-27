using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Api;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Model.Dtos;
using MediatR;

namespace LuxTravel.Api.Controllers
{
    [Route("api/master-data")]
    [ApiController]

    public class MasterDataController : ControllerBase
    {
        private readonly IMediator _mediator;

        public MasterDataController(IMediator mediator)
        {
            _mediator = mediator;
        }
        //public MasterDataController(IServiceProvider serviceProvider) : base(serviceProvider)
        //{

        //}

        [HttpGet("countries")]
        public async Task<IEnumerable<SelectedObjectDto>> GetCity([FromQuery] GetAllCitiesQuery model, CancellationToken cancellationToken = default)
        {
            var result = await _mediator.Send(model, cancellationToken);
            return result;
            //return  SendRequestAsync(model);
        }

        [HttpGet("testing")]
        public string testing()
        {
            return  "Hello World";
        }

    }
}
