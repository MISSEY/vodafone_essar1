$(function () {
    $("#grid").jqGrid({
        url: "Billed_To_Master/GetBilledToLists",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['Names'],
        colModel: [
            { key: true, name: 'Billed_To', index: 'Billed_To', editable: true },

           ],
        pager: jQuery('#pager'),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        height: "400px",
        viewrecords: true,
        caption: 'Billed To Lists',
        emptyrecords: 'No records to display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            id: "0"
        },
        autowidth: true,
        multiselect: false
    }).navGrid('#pager', { edit: true, add: true, del: true, search: true, refresh: true },
        {
            // edit options
            zIndex: 100,
            url: 'Billed_To_Master/Edit',
            closeOnEscape: true,
            closeAfterEdit: true,
            recreateForm: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // add options
            zIndex: 100,
            url: "Billed_To_Master/Create",
            closeOnEscape: true,
            closeAfterAdd: true,
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            // delete options
            zIndex: 100,
            url: "Billed_To_Master/Delete",
            closeOnEscape: true,
            closeAfterDelete: true,
            recreateForm: true,
            msg: "Are you sure you want to delete this task?",
            afterComplete: function (response) {
                if (response.responseText) {
                    alert(response.responseText);
                }
            }
        },
        {
            zIndex: 100,
            caption: "Search BilledTo",
            sopt: ['cn']
        });
});