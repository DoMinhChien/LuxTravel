var CategoryListFormViewModel = function () {
    var self = this;
    self.viewListTemplate = ko.observable('category-List');
    self.viewDetailTemplate = ko.observable('category-Detail');
    self.isViewDetail = ko.observable(false);
    self.currentViewModel = ko.observable(null);

    self.categoryListViewModel = new CategoryListViewModel(self);
};


var CategoryListViewModel = function (_parent) {
    var self = this;
    self.parent = _parent;
    self.categories = ko.observableArray([]);

    self.init = function () {
        _getCategories();
    };

    function _getCategories() {

        CommonGlobal.connectServer("Get", null, CommonEnum.API_URL.GetCategories,
            function (result) {
                self.categories(result);
            });
    };
    self.searchProduct = function () {
        _getCategories();
    };
    self.ViewCategory = function (categoryId) {
        CommonGlobal.connectServer("Get", { CategoryId: categoryId }, CommonEnum.API_URL.GetCategoryDetail,
            function (data) {
                self.parent.currentViewModel(new CategoryModel(data, self));
                $('#categoryModal').modal('show');
            });

    };
 
    self.InsertCategory = function () {
        var model = new CategoryModel({
            Id:0
        }, self);
        self.parent.currentViewModel(model);
        $('#categoryModal').modal('show');
    };
    self.init();

};