//Page OfferFilter
var OfferFilterViewModel = function (data, parent) {
    var self = this;
    self.offers = ko.observableArray([]);
    if (data.data) {
        self.offers(data.data);
    }
    self.userFilters = data.UserFilters;
    self.getUrl = function (obj) {
        var urlParam = [];
        urlParam.push("hotelId=" + obj.HotelId);
        for (var i in self.userFilters) {
            var val = encodeURI(self.userFilters[i]);
            if (i ==="DateFrom" || i ==="DateTo") {
                val = CommonGlobal.convertDateJSToClientDateTime(val, 'DD-MMM-YYYY');
            }

            urlParam.push(encodeURI(i) + "=" + val);
        }
        window.location = CommonEnum.API_URL.OfferDetail + "?" + urlParam.join("&");
        //return CommonEnum.API_URL.OfferDetail + '/' + '?hotelId=' + Id;
    };
};


//Page Offer Detail
var OfferDetailFormViewModel = function (data, parent) {
    var self = this;
    self.viewOfferDetailTemplate = ko.observable('offer-Detail-form');
    self.viewCartTemplate = ko.observable('cart-form');
    self.isViewCart = ko.observable(false);
    self.currentViewModel = ko.observable(null);
    self.userFilters = data.UserFilters;
    self.hotel = ko.observable(null);

    self.init = function() {
        if (data.data) {
            var obj = new OfferModel(data.data, self);
            self.hotel(obj);
        }
    };
    self.getImageUrl = function (url) {

        return "url(" + url + ")";
    };
    self.init();
};

// User for OfferDetail
var OfferModel = function (data, parent) {
    var self = this;
    self.parent = parent;

    self.hotelId = ko.observable(data && data.Id ? data.Id : null);
    self.guestId = ko.observable(data && data.GuestId ? data.GuestId : null);
    self.hotelName = ko.observable(data && data.Name ? data.Name : '');
    self.rooms = ko.observableArray( []);
    self.cityName = ko.observable(data && data.CityName ? data.CityName : '');
    self.address = ko.observable(data && data.Address ? data.Address : '');
    self.selectedRooms = ko.observableArray([]);
    self.ListImageName = ko.observableArray(data && data.ListImageName ? data.ListImageName : []);
    function convertToRoomModel() {
        if (data && data.Rooms ) {
            _.each(data.Rooms,
                function(item) {
                    var model = new RoomModel(item, self);
                    self.rooms.push(model);
                });
        }
    }

    self.filters = parent && parent.userFilters ? parent.userFilters : null;



    self.viewOfferDetail = function (obj) {
        self.selectedRooms.push(obj);    
        var offerDetail = new OfferDetailModel(self.filters, self, obj);
        self.parent.currentViewModel(offerDetail);
        self.parent.isViewCart(true);



    };
    //OfferModel.prototype.toJSON = function () {

    //    var obj = {
    //        HotelId: ko.utils.unwrapObservable(self.hotelId()),
    //        DateFrom: fromDate,
    //        DateTo: toDate

    //    };
    //    return obj;

    //};
    convertToRoomModel();
}

var OfferDetailModel = function (data, parent,selectedObj) {
    var self = this;
    self.parent = parent;
    self.hotelId = ko.observable(parent && parent.hotelId ? parent.hotelId() : null);
    self.hotelName = ko.observable(parent && parent.hotelName ? parent.hotelName() : null);
    var dateFrom = new Date(CommonGlobal.convertDateJSToClientDateTime(data.DateFrom, 'DD-MMM-YYYY'));
    var dateTo = new Date(CommonGlobal.convertDateJSToClientDateTime(data.DateTo, 'DD-MMM-YYYY'));
    self.dateFrom = ko.observable(dateFrom);
    self.dateTo = ko.observable(dateTo);
    self.userViewModel = ko.observable(new UserModel({}, self));

    self.roomInfor = ko.observable('');
    
    
    self.guestCount = ko.observable(data.GuestCount);
    self.roomCount = ko.computed(function () {
        return Math.ceil(self.guestCount() / selectedObj.GuestPerBed());
    });
    self.rate = ko.observable(selectedObj && selectedObj.Price ? selectedObj.Price() : null);

    self.selectedRooms = ko.observableArray([]);

    function CalculateInfo() {
        for (var i = 0; i < data.RoomCount; i++) {
            self.selectedRooms.push(selectedObj);
        }
        self.roomInfor(selectedObj.BedPerRooms() + " " + selectedObj.TypeOfRoom());

    }
    

    //Display Information Only
    self.formattedDateFrom = ko.observable(CommonGlobal.convertDateJSToClientDateTime(dateFrom, 'DD-MMM-YYYY'));
    self.formattedDateTo = ko.observable(CommonGlobal.convertDateJSToClientDateTime(dateTo, 'DD-MMM-YYYY'));
    self.nightCount = ko.computed(function() {
        var diffTime = Math.abs(self.dateTo() - self.dateFrom());
        var diffDays = Math.ceil(diffTime / (1000 * 60 * 60 * 24));
        return diffDays;
    });
    self.total = ko.computed(function() {
        return self.nightCount() * self.roomCount() * self.rate();
    })

    //API
    self.bookRoom = function () {
        var offer = self.toJSON();
        CommonGlobal.connectServer("POST",
            { offer: offer },
            CommonEnum.API_URL.ProcessOffer,
            function (data) {
                CommonGlobal.showSuccessMessage('Success', CommonEnum.API_URL.Index);

            });
    };



    function initAccordions() {
        if ($('.accordion').length) {
            var accs = $('.accordion');

            accs.each(function () {
                var acc = $(this);

                if (acc.hasClass('active')) {
                    var panel = $(acc.next());
                    var panelH = panel.prop('scrollHeight') + "px";

                    if (panel.css('max-height') == "0px") {
                        panel.css('max-height', panel.prop('scrollHeight') + "px");
                    }
                    else {
                        panel.css('max-height', "0px");
                    }
                }

                acc.on('click', function () {
                    if (acc.hasClass('active')) {
                        acc.removeClass('active');
                        var panel = $(acc.next());
                        var panelH = panel.prop('scrollHeight') + "px";

                        if (panel.css('max-height') == "0px") {
                            panel.css('max-height', panel.prop('scrollHeight') + "px");
                        }
                        else {
                            panel.css('max-height', "0px");
                        }
                    }
                    else {
                        acc.addClass('active');
                        var panel = $(acc.next());
                        var panelH = panel.prop('scrollHeight') + "px";

                        if (panel.css('max-height') == "0px") {
                            panel.css('max-height', panel.prop('scrollHeight') + "px");
                        }
                        else {
                            panel.css('max-height', "0px");
                        }
                    }
                });
            });
        }
    }

    setTimeout(function () {
        initAccordions();
    }, 1000);
    CalculateInfo();

    OfferDetailModel.prototype.toJSON = function () {
        var arr = [];
        if (self.selectedRooms().length >0) {
            _.each(self.selectedRooms(),
                function (item) {
                    var obj = item.toJSON();
                    arr.push(obj);
                });
        }
        var roomBookedModel = {
            RoomId: self.selectedRooms()[0].Id(),
            Rate: ko.utils.unwrapObservable(self.total())
        };
        
        var obj = {
            DateFrom: ko.utils.unwrapObservable(self.formattedDateFrom()),
            DateTo: ko.utils.unwrapObservable(self.formattedDateTo()),
            HotelId: ko.utils.unwrapObservable(self.hotelId()),
            RoomCount: ko.utils.unwrapObservable(self.roomCount()),
            
            GuestCount: ko.utils.unwrapObservable(self.guestCount()),
            StatusId: CommonEnum.OrderStatusEnum.Accepted,
            RoomBookedModel: roomBookedModel
            
        };
        return obj;

    };


};

var UserModel = function (data, parent) {
    var self = this;
    self.parent = parent;
    self.name = ko.observable(data && data.Name ? data.Name : 'Chien');
    self.email = ko.observable(data && data.Email ? data.Email : 'dominhchien206@gmail.com');
    self.phone = ko.observable(data && data.Phone ? data.Phone : '0798583813');

    
}
