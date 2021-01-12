using System;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using CommonFunctionality.Api;
using CommonFunctionality.Core;
using LuxTravel.Api.Core.Commands;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Model.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace LuxTravel.Api.Controllers
{
    [Route("api/user")]
    public class UserController : ApiControllerBase
    {
        public UserController(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            
        }
        [HttpPost("login")]
        public async Task<ResponseBase<string>> Login(AuthenticationQuery query)
        {
            var result = await SendRequestAsync(query);

            return result;
        }

        [HttpPost("register")]
        public async Task<bool> Register(CreateUserCommand command)
        {
            var result = await SendRequestAsync(command);

            return result;
        }

        [HttpGet("current-user")]
        public  Task<UserDto> CurrentUser()
        {
            var userId = this.User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                throw new BusinessException(Constants.Constants.NOT_AUTHENTICATED);
            }
            var email = this.User.FindFirstValue(ClaimTypes.Email);
            var name = this.User.FindFirstValue(ClaimTypes.Name);
            var address = this.User.FindFirstValue(ClaimTypes.StreetAddress);
            var phone = this.User.FindFirstValue(ClaimTypes.HomePhone);
            var gender = this.User.FindFirstValue(ClaimTypes.Gender);

            return Task.FromResult(new UserDto()
            {
                Id = Guid.Parse(userId),
                Email = email,
                DisplayName = name,
                Address = address,
                Phone =   phone,
                Gender = gender
            });
        }
    }
}
