var RateModel = function(data, parent) {
    var self = this;
    self.Name = ko.observable(data && data.Name ? data.Name : '');
    self.Rate = ko.observable(data && data.Rate ? data.Rate : '');
    self.Icon = ko.observable(getIcon(data.RateTypeModel));
    function getIcon(rateTypeModel) {
        if (rateTypeModel) {
            return rateTypeModel.Icon;
        }
        return'';
    }

};