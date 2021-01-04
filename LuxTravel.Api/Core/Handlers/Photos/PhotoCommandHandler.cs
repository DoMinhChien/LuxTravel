using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using CommonFunctionality.Core;
using LuxTravel.Api.Core.Commands;
using LuxTravel.Api.Core.Queries;
using LuxTravel.Model.BaseRepository;
using LuxTravel.Model.Dtos;
using LuxTravel.Model.Entites;
using MediatR;

namespace LuxTravel.Api.Core.Handlers.Photos
{
    public class PhotoCommandHandler : RequestHandlerBase,
        IRequestHandler<UploadPhotoCommand, bool>
    {
        private readonly Cloudinary _cloudinary;
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public PhotoCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            var cloudinaryAccount = new Account()
            {
                Cloud = "minhchien206",
                ApiKey = "531434724358744",
                ApiSecret = "dF-TvO94yoSSF6L5pIJGxVae5U8"
            };
            _cloudinary = new Cloudinary(cloudinaryAccount);

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
