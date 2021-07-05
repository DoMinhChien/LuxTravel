using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Core.Constant;
using Data;
using Model.Models;
using Service.Interfaces;

namespace Mvc.Areas.Admin.Controllers
{
    public class RoomController : BaseController
    {
        // GET: Admin/Room
        private readonly IRoomService _roomService;
        private readonly CommonConstant commonConstant;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
            commonConstant = new CommonConstant();
            
        }
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult UpdateRoom(RoomModel model)
        {
            
            //if (model.Images.Any())
            //{
            //    foreach (var img in model.Images)
            //    {
            //        string base64Str = img.ImageBase64Str.Split(',')[1];
            //        var filePath = Path.Combine(Server.MapPath(commonConstant.filePath), img.ImageName);
            //        var bytes = Convert.FromBase64String(base64Str);
            //        img.ImageByte = bytes;
            //        img.ImagePath = filePath;
            //        using (var imageFile = new FileStream(filePath, FileMode.Create))
            //        {

            //            imageFile.Write(bytes, 0, bytes.Length);
            //            imageFile.Flush();
            //        }

            //    }
            //}



            var result = _roomService.UpdateRoom(model);
            return Json(result);
        }

        public JsonResult DeleteRoom(Guid id)
        {
            var result = _roomService.DeleteRoom(id);
            return Json(result);
        }

        [HttpGet]
        public JsonResult GetRoomDetail(Guid Id)
        {
            var data = _roomService.GetRoomDetail(Id);

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult ImageUpload(ImageStoreModel model)
        {
            var file = model.ImageFile;

            if (file != null)
            {
                var filePath = Path.Combine(Server.MapPath(commonConstant.filePath), file.FileName);
                file.SaveAs(filePath);

                BinaryReader reader = new BinaryReader(file.InputStream);

                model.ImageByte = reader.ReadBytes(file.ContentLength);
                _roomService.UploadImage(model, filePath);

            }

            return Json(file.FileName, JsonRequestBehavior.AllowGet);

        }

    }
}