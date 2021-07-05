var SwitchComponent = function (params) {
    var self = this;
    self.IsActive = params.IsActive;
};



var elemInstance = document.getElementById('switch-component');



ko.components.register('switch-component', {
    viewModel: SwitchComponent,
    template: { element: elemInstance }

});