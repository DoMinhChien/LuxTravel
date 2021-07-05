var SupplierModel = function (data, parent) {
    var self = this;

    self.parentViewModel = parent;
    self.Id = ko.observable(data && data.Id ? data.Id : 0);
    self.Name = ko.observable(data && data.Name ? data.Name : '');
    self.Description = ko.observable(data && data.Description ? data.Description : '');
    self.Products = ko.observableArray(data && data.Products ? data.Products : []);
    self.IsActive = ko.observable(data && data.IsActive ? data.IsActive : false);
    self.Address = ko.observable(data && data.Address ? data.Address : '');
    self.Phone = ko.observable(data && data.Phone ? data.Phone : '');
    self.Country = ko.observable(data && data.Country ? data.Country : '');
    self.ToggleSwitch = function () {
        var a = 0;
    };
    self.UpdateSupplier = function () {
        let url = "";
        if (self.Id()) {
            url = CommonEnum.API_URL.UpdateSupplier;

        }
        else {
            url = CommonEnum.API_URL.InsertSupplier;
        }
        var model = self.toJSON();
        CommonGlobal.connectServer("POST", { Input: model }, url,
            function (data) {

                CommonGlobal.showSuccessMessage('Sucessfully', CommonEnum.API_URL.Index);
            });


    };
    self.DeleteSupplier = function (categoryId) {
        CommonGlobal.showConfirmMessage('warning',
            function () {
                CommonGlobal.connectServer("POST", { SupplierId: categoryId }, CommonEnum.API_URL.DeleteSupplier,
                    function (data) {
                        CommonGlobal.showSuccessMessage('Deleted', CommonEnum.API_URL.Index);
                    });
            });
    };

    SupplierModel.prototype.toJSON = function () {
        return {
            Id: ko.utils.unwrapObservable(self.Id()),
            Name: ko.utils.unwrapObservable(self.Name()),
            Description: ko.utils.unwrapObservable(self.Description()),
            Address: ko.utils.unwrapObservable(self.Address()),
            Phone: ko.utils.unwrapObservable(self.Phone()),
            Country: ko.utils.unwrapObservable(self.Country()),
            IsActive: ko.utils.unwrapObservable(self.IsActive())
        };

    };
};
