using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model.Models;

namespace Service.Interfaces
{
    public interface IRoomService
    {
        List<SelectedItemModel> GetListRoomStatus();
        List<RoomTypeModel> GetListRoomType();
        bool UpdateRoom(RoomModel model);
        bool DeleteRoom(Guid id);
        List<ImageStoreModel> GetListImageByRoomId(Guid roomId);
        RoomModel GetRoomDetail(Guid id);
        bool UploadImage(ImageStoreModel model, string filePath);
        List<string> GetListImageName(Guid roomId);
    }
}
