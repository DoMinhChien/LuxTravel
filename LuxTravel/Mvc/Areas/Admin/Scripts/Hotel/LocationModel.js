var LocationModel = function(data,parent,root) {
    var self = this;

    self.Id = ko.observable(data && data.Id ? data.Id : null);
    self.CityId = ko.observable(data && data.CityId ? data.CityId : undefined);
    self.DistrictId = ko.observable(data && data.DistrictId ? data.DistrictId : undefined);
    self.WardId = ko.observable(data && data.WardId ? data.WardId : undefined);
    self.ListCity = ko.observableArray(root && root.listCity ? root.listCity : []);
    self.ListDistrict = ko.observableArray(root && root.listDistrict ? root.listDistrict : []);
    self.ListWard = ko.observableArray(root && root.listWard ? root.listWard : []);

    self.CityId.subscribe(function (val) {
        CommonGlobal.connectServer("Get", { cityId: val },
            CommonEnum.API_URL.GetListDistrictByCityId,
            function (data) {
                self.ListDistrict(data);
            });
    });

    self.DistrictId.subscribe(function (val) {
        CommonGlobal.connectServer("Get", { districtId: val },
            CommonEnum.API_URL.GetListWardByDistrictId,
            function (data) {
                self.ListWard(data);
            });
    });
    LocationModel.prototype.toJSON = function() {
        return {
            Id: ko.utils.unwrapObservable(self.Id()),
            CityId: ko.utils.unwrapObservable(self.CityId()),
            DistrictId: ko.utils.unwrapObservable(self.DistrictId()),
            WardId: ko.utils.unwrapObservable(self.WardId())
        };
    };


}