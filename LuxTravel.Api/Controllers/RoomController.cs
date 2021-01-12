using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CommonFunctionality.Api;
using LuxTravel.Api.Core.Commands;
using Microsoft.AspNetCore.Mvc;

namespace LuxTravel.Api.Controllers
{
    public class RoomController : ApiControllerBase
    {
        public RoomController(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        [HttpPost("api/room")]
        public async Task<bool> Create(CreateRoomCommand command)
        {
            var result = await SendRequestAsync(command);

            return result;
        }
    }
}
