using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Service.Interfaces;
using Service.Services;

namespace Mvc.Areas.Admin.Controllers
{
    public class AdminController : Controller
    {
        private readonly ICommonService _commonService;
        private readonly IRoomService _roomService;

        public AdminController(ICommonService commonService, IRoomService roomService)
        {
            _roomService = roomService;
            _commonService = commonService;
        }
        // GET: Admin/Admin
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListCity()
        {
            var data = _commonService.GetListCity();

            return Json(data, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetListDistrict()
        {
            var data = _commonService.GetListDistrict();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetListWard()
        {
            var data = _commonService.GetListWard();

            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetListDistrictByCityId(int cityId)
        {
            var data = _commonService.GetListDistrictByCityId(cityId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetListWardByDistrictId(int districtId)
        {
            var data = _commonService.GetListWardByDistrictId(districtId);
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetMasterDataForRoom()
        {
            var roomStatuses = _roomService.GetListRoomStatus();
            var roomTypes = _roomService.GetListRoomType();


            return Json(new { roomStatuses, roomTypes }, JsonRequestBehavior.AllowGet);
        }
    }
}