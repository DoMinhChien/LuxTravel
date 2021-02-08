using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonFunctionality.Core.Behaviors;
using LuxTravel.Api.Core.Commands;
using LuxTravel.Model.BaseRepository;
using LuxTravel.Model.Entites;
using MediatR;
using Microsoft.Extensions.Configuration;

namespace LuxTravel.Api.Core.Handlers.Photos
{
    public class PhotoCommandHandler : RequestHandlerBase,
        IRequestHandler<UploadPhotoCommand, bool>
    {
        private readonly Cloudinary _cloudinary;
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();
        private readonly IConfiguration _configuration;
        public PhotoCommandHandler(IServiceProvider serviceProvider,
            IConfiguration configuration) : base(serviceProvider)
        {
            _configuration = configuration;
            var account = _configuration.GetSection("CloudinarySettings").Get<Account>();
            _cloudinary = new Cloudinary(account);

        }
        public async Task<bool> Handle(UploadPhotoCommand request, CancellationToken cancellationToken)
        {
            var file = request.File;
            var result = new ImageUploadResult();
            if (file.Length > 0)
            {
                using (var stream = file.OpenReadStream())
                {
                    var uploadParams = new ImageUploadParams()
                    {
                        File = new FileDescription(file.Name, stream),
                        Transformation = new Transformation().Width(500).Height(500).Crop("fill").Gravity("face")
                    };
                    result = _cloudinary.Upload(uploadParams);
                }

                var entity = new Photo()
                {
                    PublicId = result.PublicId,
                    Url = result.Url.ToString(),
                    ObjectId = request.ObjectId
                };
                if (request.IsMain)
                {
                    var existedMainPhoto = await _unitOfWork.PhotoRepository.GetMany(r => r.ObjectId == request.ObjectId && r.IsMain);
                    if (!existedMainPhoto.Any())
                    {
                        entity.IsMain = true;
                    }
                }
                _unitOfWork.PhotoRepository.Insert(entity);
                _unitOfWork.SaveChanges();

                return await Task.FromResult(true);
            }

            return await Task.FromResult(false);
        }
    }
}
