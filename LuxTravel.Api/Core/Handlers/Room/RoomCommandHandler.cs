using System;
using System.Threading;
using System.Threading.Tasks;
using CommonFunctionality.Core.Behaviors;
using LuxTravel.Api.Core.Commands;
using LuxTravel.Model.BaseRepository;
using MediatR;

namespace LuxTravel.Api.Core.Handlers.Room
{
    public class RoomCommandHandler : RequestHandlerBase,
        IRequestHandler<CreateRoomCommand, bool>
    {
        private readonly UnitOfWork _unitOfWork = new UnitOfWork();

        public RoomCommandHandler(IServiceProvider serviceProvider) : base(serviceProvider)
        {

        }

        public Task<bool> Handle(CreateRoomCommand request, CancellationToken cancellationToken)
        {
            var roomEntity = _mapper.Map<Model.Entities.Room>(request);
            roomEntity.RoomStatusId = new Guid("790893B5-DB3A-4351-ABD2-58A1C19951CD");
            _unitOfWork.RoomRepository.Insert(roomEntity);
            _unitOfWork.SaveChanges();
            return Task.FromResult(true);
        }

    }
}
