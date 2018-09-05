$(function () {
    $("#grid").jqGrid({
        url: "Sim_Number_Master/GetSimNumberLists",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['Sim Numbers', 'Status'],
        colModel: [
            { key: true, name: 'Sim_no', index: 'Sim_no', editable: true },
            { key: false, name: 'status', index: 'status', editable: true },
            
           ],
        pager: jQuery('#pager'),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        height: "400px",
        viewrecords: true,
        caption: 'Sim Number Lists',
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
            url: 'Sim_Number_Master/Edit',
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
            url: "Sim_Number_Master/Create",
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
            url: "Sim_Number_Master/Delete",
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
            caption: "Search Sim Number",
            sopt: ['cn']
        });
});