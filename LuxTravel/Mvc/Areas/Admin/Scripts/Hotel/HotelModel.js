
var HotelModel = function (data, parent) {
    var self = this;
    self.parent = parent;
    self.parentViewModel = parent;

    //Properties
    self.Id = ko.observable(data && data.Id ? data.Id : null);
    self.Name = ko.observable(data && data.Name ? data.Name : '');
    self.Email = ko.observable(data && data.Email ? data.Email : '');

    self.IsActive = ko.observable(data && data.IsActive ? data.IsActive : false);
    self.IsNewMode = ko.observable(data && data.IsNewMode ? data.IsNewMode : false);
    self.Quantity = ko.observable(data && data.Quantity ? data.Quantity : 0);
    //Model
    self.rooms = ko.observableArray(data && data.Rooms ? data.Rooms: []);
    self.LocationModel = ko.observable(null);
    self.roomModel = ko.observable(null);


    //Master data for Dropdown
    self.RoomStatues = ko.observableArray([]);
    self.RoomTypes = ko.observableArray([]);

    self.increase = function (object) {
        object.selectedQuantiy(object.selectedQuantiy() + 1);
    };

    self.decrease = function (object) {
        if (object.selectedQuantiy() > 0) {
            object.selectedQuantiy(object.selectedQuantiy() - 1);
        }

    };

    self.insertRoom = function() {
        self.roomModel(new RoomModel({}, self, parent));
        $('#roomModal').modal('show');
    };
    self.ReturnToList = function () {
        self.parentViewModel.parent.isViewDetail(false);
    };
    self.toggleEditRoom = function(obj) {
        CommonGlobal.connectServer("GET", { id: obj.Id}, CommonEnum.API_URL.GetRoomDetail,
            function (data) {
                self.roomModel(new RoomModel(data));
                $('#roomModal').modal('show');
                if (data.ListImageName.length > 0) {
                    for (var i = 0; i < data.ListImageName.length; i++) {
                        $("#uploadedImage").append('<img src="/Content/Admin/img/UploadedImage/' + data.ListImageName[i] + '" class="img-responsive thumbnail upload-img"/>');
                    }

                }
            });
    }





    //self.errors = ko.validation.group(self); 
    //var isValid = function () {
    //    self.errors.showAllMessages();
    //    return self.errors().length === 0;
    //};
    self.closeDialog = function () {
        $('.js-modal1').removeClass('show-modal1');
    };
    self.ToggleSwitch = function () {
        var a = 0;
    };
    self.UpdateHotel = function () {
        var url = "";
        if (self.Id()) {
            url = CommonEnum.API_URL.UpdateHotel;
        }
        else {
            url = CommonEnum.API_URL.InsertHotel;
        }
        var data = { input: self.toJSON() };
        CommonGlobal.connectServer("POST", data, url,
            function (data) {
                CommonGlobal.showSuccessMessage('Success', CommonEnum.API_URL.Index);
            });
    };
    self.DeleteHotel = function (id) {
        CommonGlobal.showConfirmMessage('warning',
            function () {
                CommonGlobal.connectServer("POST", { id: id() }, CommonEnum.API_URL.DeleteHotel,
                    function (data) {
                        CommonGlobal.showSuccessMessage('Deleted', CommonEnum.API_URL.Index);
                    });
            });
    };
    self.init = function () {
        CommonGlobal.connectServer("Get",
            null,
            CommonEnum.API_URL.GetMasterDataForRoom,
            function (data) {
                self.RoomTypes(data.roomTypes);
                self.RoomStatues(data.roomStatuses);
            });


        //if (data && data.Rooms) {
            //    _.each(data.Rooms,
            //        function (item) {
            //            self.rooms.push(new RoomModel(item, self));
            //        });
            //}

            self.LocationModel(new LocationModel(data.HotelLocation, self, parent));
        
    }

    HotelModel.prototype.toJSON = function () {
        var arr = [];
        if (self.rooms().length > 0) {
            _.each(self.rooms(),
                function(item) {
                    var obj = item.toJSON();
                    arr.push(obj);
                });
        }
        var obj = {
            Id: ko.utils.unwrapObservable(self.Id()),
            Name: ko.utils.unwrapObservable(self.Name()),
            Email: ko.utils.unwrapObservable(self.Email()),
            IsActive: ko.utils.unwrapObservable(self.IsActive()),
            HotelLocation: self.LocationModel().toJSON(),
            Rooms : arr

        };
        return obj;

    };
    self.init();
};
