ko.bindingHandlers.select2 = {
    after: ["options", "value"],
    init: function (el, valueAccessor, allBindingsAccessor, viewModel) {
        $(el).select2(ko.unwrap(valueAccessor()));
        ko.utils.domNodeDisposal.addDisposeCallback(el, function () {
            $(el).select2('destroy');
        });
    },
    update: function (el, valueAccessor, allBindingsAccessor, viewModel) {
        var allBindings = allBindingsAccessor();
        var select2 = $(el).data("select2");
        if ("value" in allBindings) {
            var newValue = "" + ko.unwrap(allBindings.value);
            if ((allBindings.select2.multiple || el.multiple) && newValue.constructor !== Array) {
                select2.val([newValue.split(",")]);
            }
            else {
                select2.val([newValue]);
            }
        }
    }
};


ko.bindingHandlers.valueUpdate = {
    update: function (element, valueAccessor, allBindingsAccessor) {
        var bindings = allBindingsAccessor();
        if (bindings.value != null) {
            $(element).change();
        }
    }
};
function formatCurrency(symbol, value, precision) {
    return (value < 0 ? "-" : "") + Math.abs(value).toFixed(precision).replace(/(\d)(?=(\d{3})+\.)/g, "$1,") ;
}

function rawNumber(val) {
    return Number(val.replace(/[^\d\.\-]/g, ""));
};

ko.bindingHandlers.currency = {
    symbol: ko.observable("$"),
    init: function (element, valueAccessor, allBindingsAccessor) {
        //only inputs need this, text values don't write back
        if ($(element).is("input") === true) {
            var underlyingObservable = valueAccessor(),
                interceptor = ko.computed({
                    read: underlyingObservable,
                    write: function (value) {
                        if (value === "") {
                            underlyingObservable(null);
                        } else {
                            underlyingObservable(rawNumber(value));
                        }
                    }
                });
            ko.bindingHandlers.value.init(element, function () {
                return interceptor;
            }, allBindingsAccessor);
        }
    },
    update: function (element, valueAccessor, allBindingsAccessor) {
        var symbol = ko.unwrap(allBindingsAccessor().symbol !== undefined ? allBindingsAccessor().symbol : ko.bindingHandlers.currency.symbol),
            value = ko.unwrap(valueAccessor());
        if ($(element).is("input") === true) {
            //leave the boxes empty by default
            value = value !== null && value !== undefined && value !== "" ? formatCurrency(symbol, parseFloat(value), 2) : "";
            $(element).val(value);
        } else {
            //text based bindings its nice to see a 0 in place of nothing
            value = value || 0;
            $(element).text(formatCurrency(symbol, parseFloat(value), 2));
        }
    }
};

ko.bindingHandlers.fileSrc = {
    init: function (element, valueAccessor) {
        ko.utils.registerEventHandler(element, "change", function () {
            var reader = new FileReader();

            reader.onload = function (e) {
                var value = valueAccessor();
                value(e.target.result);
            }

            reader.readAsDataURL(element.files[0]);
        });
    }
};
ko.bindingHandlers.fileUpload = {

    update: function (element, valueAccessor, allBindingsAccessor) {
        var value = ko.utils.unwrapObservable(valueAccessor())
        if (element.files.length && value) {
            var file = element.files[0];
            var url = allBindingsAccessor().url;
        }
    }
}