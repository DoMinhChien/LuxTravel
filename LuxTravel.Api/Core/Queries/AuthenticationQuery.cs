using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using  CommonFunctionality.Core;
using Microsoft.IdentityModel.Tokens;

namespace LuxTravel.Api.Core.Queries
{
    public class AuthenticationQuery : IRequest<ResponseBase<string>>
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}
