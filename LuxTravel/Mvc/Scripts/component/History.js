var HistoryComponent = function (params) {
    var self = this;
    self.objectId = params.objectId;
    self.histories = ko.observableArray([]);

    self.viewHistoryTemplate = ko.observable('history-Detail');
    self.GetHistory = function (objectId) {
        _getHistory(objectId);
        
    };
    function _getHistory(objectId) {
        CommonGlobal.connectServer("Get", { objectId: objectId}, "/Home/GetHistories",
            function (result) {
                self.histories(result);
                $('#historyModal').modal('show');
            });
    }
    

};


var elemInstance = document.getElementById('history-component');


ko.components.register('history-component', {
    viewModel: HistoryComponent,
    template: { element: elemInstance }

});