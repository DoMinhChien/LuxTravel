
var CategoryModel = function (data, parent) {
    var self = this;
    
    self.parentViewModel = parent;
    self.Id = ko.observable(data && data.Id ? data.Id : 0); 
    self.Name = ko.observable(data && data.Name ? data.Name: '');
    self.Description = ko.observable(data && data.Description ? data.Description : '');
    self.Products = ko.observableArray(data && data.Products ? data.Products : []);
    self.IsActive = ko.observable(data && data.IsActive ? data.IsActive : false);
    self.ToggleSwitch = function ()
        {
        var a = 0;
        };
    self.UpdateCategory = function () {
        let url = "";
        if (self.Id()) {
            url = CommonEnum.API_URL.UpdateCategory;
            
        }
        else {
            url = CommonEnum.API_URL.InsertCategory;
        }
        var model = self.toJSON();
        CommonGlobal.connectServer("POST", { Input: model }, url,
            function (data) {

                CommonGlobal.showSuccessMessage('Sucessfully', CommonEnum.API_URL.Index);
            });


    };
    self.DeleteCategory = function (categoryId) {
        CommonGlobal.showConfirmMessage('warning',
            function () {
                CommonGlobal.connectServer("POST", { CategoryId: categoryId }, CommonEnum.API_URL.DeleteCategory,
                    function (data) {
                        CommonGlobal.showSuccessMessage('Deleted', CommonEnum.API_URL.Index);
                    });
            });
    };

    CategoryModel.prototype.toJSON = function () {
        return {
            Id: ko.utils.unwrapObservable(self.Id()),
            Name: ko.utils.unwrapObservable(self.Name()),
            Description: ko.utils.unwrapObservable(self.Description()),
            IsActive: ko.utils.unwrapObservable(self.IsActive())
        };

    };
};
