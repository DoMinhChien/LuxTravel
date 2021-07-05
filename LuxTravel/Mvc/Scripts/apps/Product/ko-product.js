var ProductListFormViewModel = function () {
    var self = this;
    self.viewListTemplate = ko.observable('product-List');
    self.viewDetailTemplate = ko.observable('product-Detail');
    self.isViewDetail = ko.observable(false);
    self.currentViewModel = ko.observable(null);

    self.productListViewModel = new ProductListViewModel(self);

};


var ProductListViewModel = function (_parent) {
    var self = this;
    self.parent = _parent;
    self.products = ko.observableArray([]);
    self.ListCategory = ko.observableArray([]);
    self.ListSupplier = ko.observableArray([]);



    self.ViewProduct = function (productId) {
        CommonGlobal.connectServer("Get", { ProductId: productId }, CommonEnum.API_URL.GetProductDetail,
            function (data) {
                self.parent.currentViewModel(new ProductModel(data, self));
                self.parent.isViewDetail(true);
            });

    };
    self.columnDefs = ko.observableArray(

        [{ field: 'Name', displayName: 'Product Name', width: '200', cellTemplate: '<div class ="kgCellText"><span data-bind ="html : $parent.entity.Name"></span></div>' },
        { field: 'Description', displayName: 'Description', width: '250', cellTemplate: '<div class ="kgCellText"><span data-bind ="html : $parent.entity.Description"></span></div>' },
        { field: 'CategoryName', displayName: 'Category', width: '150', cellTemplate: '<div class ="kgCellText"><span data-bind ="html : $parent.entity.CategoryName"></span></div>' },
        { field: 'SupplierName', displayName: 'Supplier', width: '120', cellTemplate: '<div class ="kgCellText"><span data-bind ="html : $parent.entity.SupplierName"></span></div>' },
        { field: 'Quantity', displayName: 'Quantity', width: '100', cellTemplate: '<div class ="kgCellText"><span data-bind ="html : $parent.entity.Quantity"></span></div>' },
        { field: 'UnitsInStock', displayName: 'Unit In Stock', width: '100', cellTemplate: '<div class ="kgCellText"><span data-bind ="html : $parent.entity.UnitsInStock"></span></div>' },
        { field: 'UnitPrice', displayName: 'UnitPrice ($)', width: '150', cellTemplate: '<div class ="kgCellText"><span data-bind ="currency : $parent.entity.UnitPrice"></span></div>' },
        { field: 'ViewCounts', displayName: 'Views', width: '100', cellTemplate: '<div class ="kgCellText"><span data-bind ="html : $parent.entity.ViewCounts"></span></div>' },
        { field: 'IsActive', displayName: 'Status', width: '150', cellTemplate: '<div class ="kgCellText"><span data-bind ="html : CommonGlobal.displayStatusInfo($parent.entity.IsActive)"></span></div>' },
        { field: 'ModifiedOn', displayName: 'Modified On', width: '205', cellTemplate: '<div class ="kgCellText"><span data-bind ="html : CommonGlobal.convertDateJSToClientDateTime($parent.entity.ModifiedOn)"></span></div>' },
        { field: '', displayName: 'Action', width: '100', cellTemplate: '<div class ="kgCellText"><a href="" data-bind="click: $parent.$userViewModel.ViewProduct.bind($parent,$parent.entity.Id)" class= "standard-btn" ><i class="fa big-icon fa-pencil "></i></a ></div>' }

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
    self.GridOptions =
        {
            data: self.products,
            columnDefs: self.columnDefs(),
            autogenerateColumns: false,
            isMultiSelect: false,
            canSelectRows: false,
            disableTextSelection: true, // disables text selection in the grid.
            displaySelectionCheckbox: true, // toggles whether row selection check boxes appear
            enableColumnResize: true, // enable or disable resizing of columns
            enableRowReordering: false, // enable row reordering.
            enableSorting: ko.observable(true),
            filterOptions: {
                filterText: ko.observable(""), // the actual filter text so you can use external filtering while using the builting search box.
                useExternalFilter: false, // bypass internal filtering for instance, server side-searching/paging
            },
            footerRowHeight: 55,
            footerVisible: true, // showing the footer is enabled by default
            groups: [], // initial fields to group data by. array of strings. field names, not displayName.
            headerRowHeight: 32,
            headerRowTemplate: undefined, // define a header row template for further customization.
            jqueryUIDraggable: false, // Enables non-HTML5 compliant drag and drop using the jquery UI reaggable/droppable plugin. requires jqueryUI to work if enabled.
            jqueryUITheme: false, // enable the jqueryUIThemes
            keepLastSelected: true, // prevent unselections when multi is disabled.
            maintainColumnRatios: undefined, // defaults to true when using *'s or undefined widths. can be ovverriden by setting to false.
            multiSelect: true, // set this to false if you only want one item selected at a time
            plugins: [], // array of plugin functions to register.ke
            rowHeight: 45,
            rowTemplate: undefined, // Define a row Template to customize output
            selectedItems: ko.observableArray([]), // array, if multi turned off will have only one item in array
            selectWithCheckboxOnly: false, // set to true if you only want to be able to select with the checkbox
            showColumnMenu: true, // enables display of the menu to choose which columns to display.
            showFilter: true, // enables display of the filterbox in the column menu.
            showGroupPanel: false, // whether or not to display the dropzone for grouping columns on the fly
            sortInfo: undefined, // similar to filterInfo
            tabIndex: -1, // set the tab index of the grid. 
            useExternalSorting: false,
            watchDataItems: false, // DANGER: setting this to true will allow the grid to update individual elements as they change. In large datasets this adversely affects performance. It is disabled by default.
            // Paging 
            enablePaging: true, // enables the paging feature
            pagingOptions: self.pagingOptions
        };
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
       
        CommonGlobal.connectServer("Get", filterModel, CommonEnum.API_URL.GetProduct,
            function (data) {
                self.setPagingData(data);
            });
    }

    self.InsertProduct = function () {
        var model = new ProductModel({
            IsNewMode: true,
            ListCategory: self.ListCategory(),
            ListSupplier: self.ListSupplier(),
            ViewCounts: 0
        }, self);
        self.parent.currentViewModel(model);
        self.parent.isViewDetail(true);

    };
    self.searchProduct = function () {
        _getProducts();
    };
    self.init = function () {
        _getProducts();
        if (self.ListCategory().length === 0) {
            CommonGlobal.connectServerBackground("Get", null, CommonEnum.API_URL.GetCategoryForMasterData,
                function (data) {
                    self.ListCategory(data);
                });
        }
        if (self.ListSupplier().length ===0 ) {
            CommonGlobal.connectServerBackground("Get", null, CommonEnum.API_URL.GetSupplierForMasterData,
                function (data) {
                    self.ListSupplier(data);
                });
        }
    };

    self.init();

};