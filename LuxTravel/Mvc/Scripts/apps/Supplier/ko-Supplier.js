var SupplierListFormViewModel = function () {
    var self = this;
    self.viewListTemplate = ko.observable('supplier-List');
    self.viewDetailTemplate = ko.observable('supplier-Detail');
    self.isViewDetail = ko.observable(false);
    self.currentViewModel = ko.observable(null);

    self.supplierListViewModel = new SupplierListViewModel(self);
};


var SupplierListViewModel = function (_parent) {
    var self = this;
    self.parent = _parent;
    self.suppliers = ko.observableArray([]);

    self.init = function () {
        _getSuppliers();
    };

    function _getSuppliers() {

        CommonGlobal.connectServer("Get", null, CommonEnum.API_URL.GetSuppliers,
            function (result) {
                self.suppliers(result);
            });
    };
    self.searchProduct = function () {
        _getSuppliers();
    };
    self.ViewSupplier = function (supplierId) {
        CommonGlobal.connectServer("Get", { SupplierId: supplierId }, CommonEnum.API_URL.GetSupplierDetail,
            function (data) {
                self.parent.currentViewModel(new SupplierModel(data, self));
                $('#supplierModal').modal('show');
            });

    };

    self.InsertSupplier = function () {
        var model = new SupplierModel(self);
        self.parent.currentViewModel(model);
        $('#supplierModal').modal('show');
    };
    self.init();

};