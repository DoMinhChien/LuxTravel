var HomeFormViewModel = function (data, parent) {
    var self = this;
    self.locations = ko.observableArray([]);
    self.hotels = ko.observableArray([]);
    self.getUrl = function (Id) {
        return CommonEnum.API_URL.OfferDetail + '/' + '?hotelId=' + Id;
    };
    self.searchModel = ko.observable(null);
    self.init = function () {
        CommonGlobal.connectServer("GET",
            {},
            CommonEnum.API_URL.GetDataForHomepage,
            function (data) {
                convertToModel(data.hotels);
                self.locations(data.locations);
                self.searchModel(new SearchViewModel(data.citites));
            });
    };
    function convertToModel(listHotel) {
        _.each(listHotel,
            function (hotel) {
                var ratingArr = [];
                if (hotel.RatingIcon.substr(hotel.RatingIcon.length - 1) > 0) {
                    for (var i = 0; i < hotel.RatingIcon.substr(hotel.RatingIcon.length - 1); i++) {
                        ratingArr.push({});
                    }
                }
                var obj = {
                    Id: hotel.Id,
                    Name: hotel.Name,
                    CityName: hotel.CityName,
                    Icon: hotel.RatingIcon,
                };
                self.hotels.push(obj);
            });
    };

    self.searchOffer = function () {
        if (self.searchModel().errors().length > 0) {
            self.searchModel().errors.showAllMessages();
            return;
        }
        var filter = self.searchModel().toJSON();
        var urlParam = [];

        for (var i in filter) {
            urlParam.push(encodeURI(i) + "=" + encodeURI(filter[i]));
        }

        window.location = CommonEnum.API_URL.GetOffer + "?" + urlParam.join("&");
    }


    self.init();

};

var SearchViewModel = function (data, parent) {
    var self = this;
    self.parent = parent;
    self.cities = ko.observableArray(data ? data : []);
    self.cityName = ko.observable('HCM');
    self.cityId = ko.observable(null);
    self.guestCount = ko.observable(1);
    self.roomCount = ko.observable(1).extend({ notify: 'always' }).extend({
        validation: [
            {
                validator: function (number) {
                    return number <= self.guestCount();
                },
                message: "Số phòng không thể nhiều hơn số khách!"
            }]
    });
    self.dateFrom = ko.observable().extend({
        required: {
            message: "Vui lòng chọn ngày check in"
        }
    });
    self.dateTo = ko.observable().extend({
        required: {
            message: "Vui lòng chọn ngày check out"
        }
    });



    SearchViewModel.prototype.toJSON = function () {
        console.log(self.dateFrom());
        var obj = {
            DateFrom: CommonGlobal.convertDateJSToClientDateTime(self.dateFrom(), 'DD-MMM-YYYY'),
            DateTo: CommonGlobal.convertDateJSToClientDateTime(self.dateTo(), 'DD-MMM-YYYY'),
            RoomCount: ko.utils.unwrapObservable(self.roomCount()),
            LocationId: ko.utils.unwrapObservable(self.cityId()),
            GuestCount: ko.utils.unwrapObservable(self.guestCount())
        };
        return obj;

    };
    self.errors = ko.validation.group(self);
};