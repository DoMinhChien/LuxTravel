
var RoomModel = function (data, parent) {
    var self = this;
    self.parent = parent;
    self.Id = ko.observable(data && data.Id ? data.Id : null);
    self.Name = ko.observable(data && data.Name ? data.Name : '');
    self.HotelId = parent && parent.Id ? parent.Id : null;

    self.RoomTypeId = ko.observable(data && data.RoomTypeId ? data.RoomTypeId : null);

    self.StatusId = ko.observable(data && data.StatusId ? data.StatusId : null);
    self.Number = ko.observable(data && data.Number ? data.Number : null);
    self.RoomFloor = ko.observable(data && data.RoomFloor ? data.RoomFloor : null);

    self.ListImageName = ko.observableArray(data && data.ListImageName ? data.ListImageName: []);

    //self.Price = ko.observable(data && data.Price ? data.Price : null);
    //Temporary (need to define Model)
    if (data.Rates &&data.Rates.length >0) {
        self.Price = ko.observable(data.Rates && data.Rates[0].Rate ? data.Rates[0].Rate : null);
    } else {
        self.Price = ko.observable(null);
    }
    self.BedPerRooms = ko.observable(0);
    self.GuestPerBed = ko.observable(0);
    self.TypeOfRoom = ko.observable('');
    function getRoomType() {
        if (data.RoomTypeId == CommonEnum.RoomTypeEnum.Single) {
            self.BedPerRooms(CommonEnum.RoomTypeDetailEnum.Single.Bed);
            self.GuestPerBed(CommonEnum.RoomTypeDetailEnum.Single.Guest * self.BedPerRooms());
            self.TypeOfRoom('Giường đơn');
        }
        else if (data.RoomTypeId == CommonEnum.RoomTypeEnum.Twin) {
            self.BedPerRooms(CommonEnum.RoomTypeDetailEnum.Twin.Bed);
            self.GuestPerBed(CommonEnum.RoomTypeDetailEnum.Twin.Guest);
            self.TypeOfRoom('Giường đơn');
        }
        else if (data.RoomTypeId == CommonEnum.RoomTypeEnum.Double) {
            self.BedPerRooms(CommonEnum.RoomTypeDetailEnum.Double.Bed);
            self.GuestPerBed(CommonEnum.RoomTypeDetailEnum.Double.Guest);
            self.TypeOfRoom('Giường đôi');
        }
    }

    getRoomType();
    self.Images = ko.observableArray([]);
    self.getsrcImg = function(name) {
        return "/Content/Admin/img/UploadedImage/" + name;
    }
    self.UpdateRoom = function () {

        var model = self.toJSON();
        CommonGlobal.connectServer("POST",
            { model: model },
            CommonEnum.API_URL.UpdateRoom,
            function (data) {
                CommonGlobal.showSuccessMessage('Updated', CommonEnum.API_URL.Index);
            });
    };
    self.DeleteRoom = function (id) {
        CommonGlobal.connectServer("POST",
            { id: id() },
            CommonEnum.API_URL.DeleteRoom,
            function (data) {
                CommonGlobal.showSuccessMessage('Deleted', CommonEnum.API_URL.Index);
            });
    };

    RoomModel.prototype.toJSON = function () {
        var listImages = [];
        if (self.Images().length > 0) {
            _.each(self.Images(),
                function (item) {
                    var jsonObj = item.toJSON();
                    listImages.push(jsonObj);

                });
        }
        var obj = {
            Id: ko.utils.unwrapObservable(self.Id()),
            Name: ko.utils.unwrapObservable(self.Name()),
            RoomTypeId: ko.utils.unwrapObservable(self.RoomTypeId()),
            StatusId: ko.utils.unwrapObservable(self.StatusId()),
            Number: ko.utils.unwrapObservable(self.Number()),
            RoomFloor: ko.utils.unwrapObservable(self.RoomFloor()),
            Price: ko.utils.unwrapObservable(self.Price()),
            HotelId: ko.utils.unwrapObservable(ko.isObservable(self.HotelId) ? self.HotelId() : self.HotelId),
            Images: listImages
        };
        return obj;

    };
    self.init = function() {
        //_.each(data.Images,
        //    function (item) {
        //        var model = new ImageModel(item);
        //        self.Images.push(model);
        //    });
        
    };
    self.init();
  

    self.ReadImage = function (file) {

        var reader = new FileReader;
        var image = new Image;

        reader.readAsDataURL(file);
        reader.onload = function (_file) {

            image.src = _file.target.result;
            image.onload = function () {

                var height = this.height;
                var width = this.width;
                var type = file.type;
                var size = ~~(file.size / 1024) + "KB";

                $("#targetImg").attr('src', _file.target.result);
                $("#description").text("Size:" + size + ", " + height + "X " + width + ", " + type + "");
                $("#imgPreview").show();

            }

        }

    }

    self.ClearPreview = function () {
        $("#imageBrowes").val('');
        $("#description").text('');
        $("#imgPreview").hide();

    }

    self.Uploadimage = function () {

        var file = $("#imageBrowes").get(0).files;

        var data = new FormData;
        data.append("ImageFile", file[0]);
        data.append("ObjectId",self.Id());
        $.ajax({

            type: "Post",
            url: CommonEnum.API_URL.ImageUpload,
            data: data,
            contentType: false,
            processData: false,
            success: function (response) {
                self.ClearPreview();
                $("#uploadedImage").append('<img src="/Content/Admin/img/UploadedImage/' + response + '" class="img-responsive thumbnail upload-img"/>');
                //$("#uploadedImage").append('<img src="/UploadedImage/' + response + '" class="img-responsive thumbnail"/>');
                
                

            }

        })
        
    }
}