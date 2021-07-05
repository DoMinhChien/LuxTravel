using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using Core.Extensions;
using Model.Models;
using Mvc.Inputs;
using Service.Interfaces;

namespace Mvc.Areas.Admin.Controllers
{
    public class HotelController : BaseController
    {
        private readonly IHotelService _hotelService;
        private readonly ICommonService _commonService;
        public HotelController(IHotelService hotelService,ICommonService commonService)
        {
            this._hotelService = hotelService;
            this._commonService = commonService;
        }

        public HotelController(){}
        // GET: Admin/Hotel
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult GetListHotel()
        {

            var data = _hotelService.GetListHotel();
            var ListCity = _commonService.GetListCity();
            var ListDistrict = _commonService.GetListDistrict();
            var ListWard = _commonService.GetListWard();
            JSPagedDataResult.rows = data;
            JSPagedDataResult.Total = data.Count;
            var result = new {data = JSPagedDataResult, listCity = ListCity,listDistrict= ListDistrict ,listWard = ListWard };
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult InsertHotel(HotelModel Input)
        {
            bool result = _hotelService.InsertHotel(Input);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpPost]
        public JsonResult UpdateHotel(HotelModel Input)
        {
            bool result = _hotelService.UpdateHotel(Input.MapTo<HotelModel>());
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public JsonResult GetHotelDetail(Guid HotelId)
        {
            var serializer = new JavaScriptSerializer();
            serializer.MaxJsonLength = Int32.MaxValue;

            var data = _hotelService.GetHotelDetail(HotelId);
            //var result = new ContentResult()
            //{
            //    Content = serializer.Serialize(data),
            //    ContentType = "application/json"
            //};
            return Json(data, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteHotel(Guid id)
        {
            bool result = _hotelService.DeleteHotel(id);
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}