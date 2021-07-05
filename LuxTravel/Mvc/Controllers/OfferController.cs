using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Model.Models;
using Mvc.Areas.Admin.Controllers;
using Service.Interfaces;

namespace Mvc.Controllers
{
    public class OfferController : BaseController
    {
        private readonly IHotelService _hotelService;
        private readonly ICommonService _commonService;
        private readonly IRoomService _roomService;
        private readonly IOfferService _offerService;
        public OfferController(IHotelService hotelService,
            ICommonService commonService,
            IRoomService roomService,
            IOfferService offerService)
        {
            this._hotelService = hotelService;
            this._commonService = commonService;
            this._roomService = roomService;
            this._offerService = offerService;
        }
        // GET: Offer
        public ActionResult Index(OfferFilterModel filterModel)
        {
            var data = _offerService.GetOffer(filterModel);
            ViewBag.UserFilters = filterModel;
            return View(data);
        }

        public ActionResult Cart()
        {
            return View();
        }

        public ActionResult OfferDetail(Guid hotelId, OfferFilterModel filterModel)
        {
            var hotel = _hotelService.GetHotelDetail(hotelId);
            ViewBag.UserFilters = filterModel;
            return View(hotel);
        }
        

        public JsonResult ProcessOffer(BookingModel offer)
        {
            var result = _offerService.ProcessOffer(offer);
            return Json(true);
        }
    }
}