using System;
using System.Threading.Tasks;
using CommonFunctionality.Api;
using LuxTravel.Api.Core.Commands;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LuxTravel.Api.Controllers
{
    [Authorize]

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
