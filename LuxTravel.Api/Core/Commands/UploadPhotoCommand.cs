using System;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace LuxTravel.Api.Core.Commands
{
    public class UploadPhotoCommand : IRequest<bool>
    {
        public Guid ObjectId { get; set; }
        public IFormFile File { get; set; }
        public string Description { get; set; }
        public bool IsMain { get; set; }
    }
}
