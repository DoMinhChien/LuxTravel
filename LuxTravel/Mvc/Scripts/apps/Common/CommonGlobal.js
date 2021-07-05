//if (typeof (CommonGlobal)=='undefined') {
//    var CommonGlobal = {};
//}
var CommonGlobal = {
    Ajax: function (type, param, url, callbackSuccess,isLoading) {
        $.ajax({
            type: type == "POST"? type : "GET",
            url: url,
            beforeSend: function () {
                if (isLoading) {
                    $('#loader').show();
                }
                
            },

            data: (param),
            success: function (data) {
                callbackSuccess(data);
            },
            complete: function () {
                if (isLoading) {
                    $('#loader').hide();
                }
         
            },
            error: function () {

            }
        });
    },
    connectServer: function (type, param, url, callbackSuccess) {
        CommonGlobal.Ajax(type, param, url, callbackSuccess,true);


    },
    connectServerBackground: function (type, param, url, callbackSuccess) {
        CommonGlobal.Ajax(type, param, url, callbackSuccess,false);
    },
    convertDateJSToClientDateTime: function (dateJs, formatMatch) {
        if (dateJs === null) {
            return '';
        }
        if (!formatMatch) {
            formatMatch = "DD-MMM-YYYY hh:mm:ss A";
        }
        
        return moment(dateJs).format(formatMatch);
        
    },
    displayStatusInfo: function (statusId) {
        if (statusId) {
            return "<button type='button' class='btn custom-active-stt-btn btn-success display-mode'><span><i class='fa fa-check'></i></span> </button>";
        }
        else {
            return "<button type='button' class='btn custom-active-stt-btn btn-default display-mode'><span><i class='fa fa-times'></i></span> </button>";
        }
    },
    showSuccessMessage: function (content, returnUrl) {
        var type = 'success';
        CommonGlobal.showSweetAlert(content, type, returnUrl);
    },
    showInfoMessage: function (content, type, returnUrl) {
        CommonGlobal.showSweetAlert(content, type, returnUrl);
    },
    swalWithBootstrapButtons: function () {
        var customeAlert = Swal.mixin({
            confirmButtonClass: 'btn btn-success ',
            cancelButtonClass: 'btn btn-danger mg-l-10',
            buttonsStyling: false
        });
        return customeAlert;
    },
    showConfirmMessage: function (type, callback) {
        const sweetAlert = CommonGlobal.swalWithBootstrapButtons();

        sweetAlert.fire({
            title: '<strong class="size-25">Are you sure?</strong>',
            text: "You won't be able to revert this!",
            type: type,
            showCancelButton: true,
            confirmButtonText: 'Yes, delete it!',
            customClass: 'swal-wide',
            cancelButtonText: 'No, cancel!',
            //reverseButtons: true
        }).then((result) => {
            if (result.value) {
                callback();

            }
        });
    },
    showSweetAlert: function (content, type, returnUrl) {
        Swal.fire(
            {

                //position: 'top-end',
                title: content,
                type: type,
                customClass: 'swal-wide'
                //showConfirmButton: false,
                //timer: 1500
            }
        ).then((isConfirm) => {
            if (isConfirm.value) {
                window.location = returnUrl;
                // "/Product/Index"
            }
        });
    },
    randomString: function() {
            var result = '';
            var characters = 'ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789';
            var charactersLength = characters.length;
            for (var i = 0; i < 15; i++) {
                result += characters.charAt(Math.floor(Math.random() * charactersLength));
            }
            return result;

    },
    getGridOptions: function(data, columnDef, pagingOption) {
        return {
            data: data,
            columnDefs: columnDef,
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
            pagingOptions: pagingOption
        }
    }


};