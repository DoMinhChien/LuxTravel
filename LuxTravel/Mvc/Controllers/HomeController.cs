using System.Web.Mvc;
using Mvc.Areas.Admin.Controllers;
using Service.Interfaces;

namespace Mvc.Controllers
{
    public class HomeController : BaseController
    {
        private readonly IHotelService _hotelService;
        private readonly ICommonService _commonService;
        private readonly IRoomService _roomService;
        public HomeController(IHotelService hotelService, ICommonService commonService, IRoomService roomService)
        {
            this._hotelService = hotelService;
            this._commonService = commonService;
            this._roomService = roomService;
        }
        // GET: Home
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Offer()
        {
            return View();
        }

        public JsonResult GetDataForHomepage() 
        {
            var locations = _hotelService.GetTopLocation();
            var hotels = _hotelService.GetListHotelForHomepage();
            var citites = _commonService.GetListCity();
            return Json(new {hotels,locations,citites}, JsonRequestBehavior.AllowGet);
        }

    }
}