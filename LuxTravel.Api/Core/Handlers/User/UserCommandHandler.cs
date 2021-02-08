using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Core;
using CommonFunctionality.Core.Behaviors;
using LuxTravel.Api.Core.Commands;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Model.BaseRepository;
using LuxTravel.Model.Entities;
using MediatR;
using Microsoft.IdentityModel.Tokens;
using Microsoft.Extensions.Configuration;
namespace LuxTravel.Api.Core.Handlers.User
{
    public class UserCommandHandler : RequestHandlerBase,
        IRequestHandler<CreateUserCommand, bool>,
        IRequestHandler<AuthenticationQuery, ResponseBase<string>>
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private readonly IConfiguration _configuration;

        public UserCommandHandler(IServiceProvider serviceProvider,
            IConfiguration configuration) : base(serviceProvider)
        {
            _configuration = configuration;
        }
        public Task<bool> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            CreatePasswordHash(request.Password, out byte[] passwordHash, out byte[] passwordSalt);
            var user = new Guest()
            {
                Name = request.Name,
                Address = request.Address,
                Phone = request.Phone,
                Email = request.Email,
                PasswordHash = passwordHash,
                PasswordSalt = passwordSalt
            };

            _unitOfWork.GuestRepository.Insert(user);
            _unitOfWork.SaveChanges();
            return Task.FromResult(true);
        }

        public async Task<ResponseBase<string>> Handle(AuthenticationQuery request, CancellationToken cancellationToken)
        {

            var entity =  await _unitOfWork.GuestRepository.GetMany(x => x.Email == request.Email);
            var user = entity.FirstOrDefault();
            if (user != null)
            {
                LoginValidation(user, request.Password);
                var claims = new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email),
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim(ClaimTypes.StreetAddress, user.Address),
                    new Claim(ClaimTypes.HomePhone, user.Phone),
                    new Claim(ClaimTypes.Gender, user.Male == true? "Male" : "Female"),

                };

                var appSetting = _configuration.GetSection("AppSettings:Token").Value;
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(appSetting));

                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
                var tokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(claims),
                    SigningCredentials = creds
                };

                var tokenHandler = new JwtSecurityTokenHandler();

                var token = tokenHandler.CreateToken(tokenDescriptor);
                return new ResponseBase<string>(tokenHandler.WriteToken(token));
            }

            return null;
        }

        private void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            var hmac = new System.Security.Cryptography.HMACSHA512();
            passwordHash = hmac.Key;
            passwordSalt = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
        }

        private void LoginValidation(Guest entity, string password)
        {
            if (entity == null)
            {
                throw new BusinessException(Constants.Constants.NOT_FOUND_CODE);
            }

            //if (!VerifyPasswordHash(password , entity.Password, entity.PasswordHash, entity.PasswordSalt))
            //{ 
            //throw new BusinessException(Constants.Constants.NOT_FOUND_CODE);
            //}
        }
        
        private static bool VerifyPasswordHash(string password, string userPassword, byte[] storedHash, byte[] storedSalt)
        {

            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return password.Equals(userPassword);
        }
    }
}
