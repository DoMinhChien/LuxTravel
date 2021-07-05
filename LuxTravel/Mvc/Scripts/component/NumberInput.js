var NumberInputComponent = function (params) {
    var self = this;
    self.value = params.value;
    self.min = params.min || 0;
};
var elemInstance = document.getElementById('number-input-component');

ko.components.register('number-input-component', {
    viewModel: NumberInputComponent,
    template: { element: elemInstance }

})