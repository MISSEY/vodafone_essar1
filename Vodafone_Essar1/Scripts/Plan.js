$(function () {
    $("#grid").jqGrid({
        url: "Plans_Master/GetPlanLists",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['Level', 'Plan', 'Limit', 'Description'],
        colModel: [
            { key: true, name: 'Level', index: 'Level', editable: true },
            { key: false, name: 'Plan', index: 'Plan', editable: true },
            { key: false, name: 'Limit', index: 'Limit', editable: true },
            { key: false, name: 'Descrp', index: 'Descrp', editable: true, }
           ],
        pager: jQuery('#pager'),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        height: "400px",
        viewrecords: true,
        caption: 'Plan Lists',
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
    }).navGrid('#pager', { edit: true, add: true, del: true, search: true,refresh: true },
        {
            // edit options
            zIndex: 100,
            url: 'Plans_Master/Edit',
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
            url: "Plans_Master/Create",
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
            url: "Plans_Master/Delete",
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
                caption: "Search Plans",  
                sopt: ['cn']  
        } );

});