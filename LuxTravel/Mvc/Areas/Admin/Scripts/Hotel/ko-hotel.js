var HotelListFormViewModel = function () {
    var self = this;
    self.viewListTemplate = ko.observable('hotel-List');
    self.viewDetailTemplate = ko.observable('hotel-Detail');
    self.isViewDetail = ko.observable(false);
    self.currentViewModel = ko.observable(null);

    self.hotelListViewModel = new HotelListViewModel(self);

};  


var HotelListViewModel = function (_parent) {
    var self = this;
    self.parent = _parent;
    self.products = ko.observableArray([]);
    self.listCity = [];
    self.listDistrict = [];
    self.listWard = [];
    self.ViewProduct = function (hotelId) {
        CommonGlobal.connectServer("Get", { HotelId: hotelId }, CommonEnum.API_URL.GetHotelDetail,
            function (data) {
                self.parent.currentViewModel(new HotelModel(data, self));
                self.parent.isViewDetail(true);
            });

    };
    self.columnDefs = ko.observableArray(

        [{ field: 'Name', displayName: 'Hotel', width: '200', cellTemplate: '<div class ="kgCellText"><span data-bind ="html : $parent.entity.Name"></span></div>' },
        { field: 'Email', displayName: 'Email', width: '250', cellTemplate: '<div class ="kgCellText"><span data-bind ="html : $parent.entity.Email"></span></div>' },
        { field: 'Address', displayName: 'Address', width: '150', cellTemplate: '<div class ="kgCellText"><span data-bind ="html : $parent.entity.Address"></span></div>' },
        { field: 'Phone', displayName: 'Phone', width: '120', cellTemplate: '<div class ="kgCellText"><span data-bind ="html : $parent.entity.Phone"></span></div>' },
        //{ field: 'IsActive', displayName: 'Status', width: '150', cellTemplate: '<div class ="kgCellText"><span data-bind ="html : CommonGlobal.displayStatusInfo($parent.entity.IsActive)"></span></div>' },
        { field: 'ModifiedOn', displayName: 'Modified On', width: '205', cellTemplate: '<div class ="kgCellText"><span data-bind ="html : CommonGlobal.convertDateJSToClientDateTime($parent.entity.ModifiedOn)"></span></div>' },
        { field: '', displayName: 'Action', width: '100', cellTemplate: '<div class ="kgCellText"><a href="" title="Edit" data-bind="click: $parent.$userViewModel.ViewProduct.bind($parent,$parent.entity.Id)" class= "standard-btn" ><i class="fa big-icon fa-pencil "></i></a ></div>' }

        ]
    );
    self.sortInfo = ko.observable();


    self.pagingOptions =
        {
            currentPage: ko.observable(1), // what page they are currently on
            pageSize: ko.observable(10), // Size of Paging data
            pageSizes: ko.observableArray([10, 20, 50]), // page Sizes
            totalServerItems: ko.observable(0) // how many items are on the server (for paging)
        };
    self.GridOptions = CommonGlobal.getGridOptions(self.products, self.columnDefs(), self.pagingOptions);
 
    self.pagingOptions.currentPage.subscribe(function (data) {
        _getProducts();
    });
    self.pagingOptions.pageSize.subscribe(function (pageSize) {
        _getProducts();
    });
    self.setPagingData = function (data) {
        self.products(data.rows || []);
        self.pagingOptions.totalServerItems(data.records || 0);
    };

    var pagingFilter = function () {
        var filter = {};
        filter.PageSize = self.pagingOptions.pageSize();
        filter.PageIndex = self.pagingOptions.currentPage();

        return filter;
    };
    function _getProducts() {
        var filterModel = pagingFilter();

        CommonGlobal.connectServer("Get", filterModel, CommonEnum.API_URL.GetHotel,
            function (data) {
                self.setPagingData(data.data);
                self.listCity = data.listCity;
                self.listDistrict = data.listDistrict;
                self.listWard = data.listWard;
            });
    }

    self.InsertHotel = function () {
        var model = new HotelModel({}, self);
        self.parent.currentViewModel(model);
        self.parent.isViewDetail(true);

    };
    self.searchProduct = function () {
        _getProducts();
    };
    self.init = function () {
        _getProducts();
        //if (self.ListCategory().length === 0) {
        //    CommonGlobal.connectServerBackground("Get", null, CommonEnum.API_URL.GetCategoryForMasterData,
        //        function (data) {
        //            self.ListCategory(data);
        //        });
        //}
        //if (self.ListSupplier().length === 0) {
        //    CommonGlobal.connectServerBackground("Get", null, CommonEnum.API_URL.GetSupplierForMasterData,
        //        function (data) {
        //            self.ListSupplier(data);
        //        });
        //}
    };

    //self.init();

};