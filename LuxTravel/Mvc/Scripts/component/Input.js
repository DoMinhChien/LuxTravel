var InputComponent = function(params) {
    var self = this;
    self.isEditting = ko.observable(params.isEditting || true);
    self.name = params.name;
    self.maxLength = params.maxLength;
    self.placeholder = params.placeholder;
    self.isCurrency = (params.isCurrency || false);
    self.CheckValidInput = function (value, e) {

    };
};
var elemInstance = document.getElementById('input-component');



ko.components.register('input-component', {
    viewModel: InputComponent,
    template: { element: elemInstance }

})