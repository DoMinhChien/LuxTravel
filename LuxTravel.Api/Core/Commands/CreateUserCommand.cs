using MediatR;

namespace LuxTravel.Api.Core.Commands
{
    public class CreateUserCommand : IRequest<bool>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Address { get; set; }
        public string Phone { get; set; }
        public bool Male { get; set; }
    }
}
