using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CommonFunctionality.Api;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Model.BaseRepository;
using LuxTravel.Model.Dtos;

namespace LuxTravel.Api.Controllers
{
    [Route("api/master-data")]
    [ApiController]

    public class MasterDataController : ApiControllerBase
    {
        private UnitOfWork unitOfWork = new UnitOfWork();


        public MasterDataController(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        [HttpGet("cities")]
        public async Task<IEnumerable<SelectedObjectDto>> GetCity([FromQuery] GetAllCitiesQuery model)
        {

            return await  SendRequestAsync(model);
        }
        [HttpGet("room-types")]
        public async Task<IEnumerable<RoomTypeDto>> GetRoomType([FromQuery] GetAllRoomTypesQuery model)
        {

            return await SendRequestAsync(model);
        }

    }
}
