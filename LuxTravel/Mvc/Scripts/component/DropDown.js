

var DropdownComponent = function (params) {
    var self = this;

    var id = (new Date().valueOf()).toString() + "_" + CommonGlobal.randomString();
    self.id = ko.observable(id);
    self.disabled = params.disabled;
    self.options = params.options || ko.observableArray([]);
    self.optionsText = params.optionsText;
    self.optionsValue = params.optionsValue;
    self.value = params.value;

    var isRequire = typeof params.isRequire === "function" ? params.isRequire() : params.isRequire;
    var isMultiSelect = typeof params.isMultiSelect === "function" ? params.isMultiSelect() : false;

    var InitComponent = function () {
        var element = $("#" + self.id() + " select.select2");

        if (!params.placeholder) params.placeholder = ' ';

        element.select2({
            placeholder: params.placeholder
        });

        $('#' + self.id() + ' select.multipleSelect').on('select2:select', function (event) {
            self.setValueMultiSelect(event);
        });

        $('#' + self.id() + ' select.multipleSelect').on('select2:unselect', function (event) {
            self.setValueMultiSelect(event);
        });

        element.on('select2:open', function (event) {
            $('#' + self.id()).addClass('dropdown-open');
        });

        element.on('select2:close', function (event) {
            $('#' + self.id()).removeClass('dropdown-open');
        });

        if (isMultiSelect) {
            setDropdownValue(params.multipleSelect());
        }
        else {
            setDropdownValue(params.value());
            if (self.style) {
                var style = element.attr("style");
                $("#" + self.id() + " .select2-selection").attr("style", style);

                //Change css after select 2 init
                $("#" + self.id() + " button.caret-down").css("opacity", 0.5);
            }
        }

        $("#" + self.id() + " .icon-group-multipleSelect").show();
        self.checkShowIconRemove();
    }

    self.isMultiSelect = ko.observable(isMultiSelect);

    var setDropdownValue = function (value) {
        $('#' + self.id() + " select.select2").val(value).trigger('change');
    }

    var subscribeValue = function (value) {
        setDropdownValue(value);
    }
    //CommonGlobal.ReSubscribeCallback(params.multipleSelect, subscribeValue);
    //CommonGlobal.ReSubscribeCallback(params.value, subscribeValue);

    if (params.multipleSelect == null || typeof params.multipleSelect === "function" && params.multipleSelect() == null) {
        params.multipleSelect = ko.observableArray([]);
    }

    var subscribeOptions = function (options) {
        var value = isMultiSelect ? params.multipleSelect() : params.value();
        setDropdownValue(value);
    };
    //CommonGlobal.ReSubscribeCallback(params.options, subscribeOptions);

    self.style = params.style;
    if (self.style) {
        for (var key in self.style) {
            if (self.style[key] && typeof self.style[key].subscribe === 'function') {
                var keyname = key;
                var subscribeStyle = function (value) {
                    $("#" + self.id() + " .select2-selection").css(keyname, value);
                }
                //CommonGlobal.ReSubscribeCallback(self.style[keyname], subscribeStyle);
            }
        }
    }
    if (typeof (params.defaultValue) === "function") {
        self.defaultValue = typeof (params.defaultValue) === 'function' ? params.defaultValue() : null;
    }

    var multipleSelect = typeof params.multipleSelect === "function" ? params.multipleSelect() : params.multipleSelect;
    self.multipleSelect = params.multipleSelect;

    self.checkShowIconRemove = function () {
        var isShowIconRemove = false;
        if (isMultiSelect) {
            isShowIconRemove = !isRequire && self.disabled() === false && self.multipleSelect && self.multipleSelect() && self.multipleSelect().length > 0;
        }
        else {
            isShowIconRemove = !isRequire && self.disabled() === false && ((self.value() && self.value() != "null") || self.value() == 0);
        }

        if (isShowIconRemove) {
            $("#" + self.id() + " i.multipleSelect").show();
        }

        return isShowIconRemove;
    }


    self.isShowIconRemove = ko.computed(function () {
        return self.checkShowIconRemove();
    });

    self.setValueMultiSelect = function (event) {
        var selectedValues = $(event.currentTarget).val() || [];
        self.multipleSelect(selectedValues);
    }

    setTimeout(function () {
        InitComponent();
    }, 5);

    self.removeItem = function (data, event) {
        if (isMultiSelect) {
            setDropdownValue(null);
            self.multipleSelect([]);
        }
        else {
            params.value(self.defaultValue);
            setDropdownValue(self.defaultValue);
        }
    }

    self.toggleDropdown = function (data, event) {
        $("#" + self.id() + " select.select2").select2('open');
    }
}

var elemInstance = document.getElementById('dropdown-component');

ko.components.register('dropdown-component', {
    viewModel: DropdownComponent,
    template: { element: elemInstance }
});