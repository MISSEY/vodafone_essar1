$(function () {
    $("#grid").jqGrid({
        url: "Essar_Users_Master/GetEssarUsers",
        datatype: 'json',
        mtype: 'Get',
        colNames: ['Sr No', 'Employee Name', 'SAP ID', 'Mobile number', 'Sim number', 'Email id', 'Levels', 'Limit', 'Billed To', 'Department', 'Current Scheme','Status'],
        colModel: [

            { key: true,  hidden:true, name: 'Sr_no', index: 'Sr_no', editable: true },
            { key: false, name: 'Employees_Name', index: 'Employees_Name', editable: true },
            { key: false, name: 'SAP_ID', index: 'SAP_ID', editable: true },
            { key: false, name: 'Mobile_no', index: 'Mobile_no', editable: true },
            { key: false, name: 'Sim_no', index: 'Sim_no', editable: true, },
            { key: false, name: 'email_id', index: 'email_id', editable: true, },
            { key: false, name: 'Levels', index: 'Levels', editable: true, },
            { key: false, name: 'Limit', index: 'Limit', editable: true, },
            { key: false, name: 'Billed_To', index: 'Billed_To', editable: true, },
            { key: false, name: 'Department', index: 'Department', editable: true, },
            { key: false, name: 'Scheme', index: 'Scheme', editable: true, },
            { key: false, name: 'status', index: 'status', editable: true, }

           ],
        pager: jQuery('#pager'),
        rowNum: 10,
        rowList: [10, 20, 30, 40],
        height:"400px",
        viewrecords: true,
        caption: 'Employee Lists',
        emptyrecords: 'No records to display',
        jsonReader: {
            root: "rows",
            page: "page",
            total: "total",
            records: "records",
            repeatitems: false,
            id: "0"
        },
       autowidth:true,
        multiselect: false
    }).navGrid('#pager', { edit: true, add: true, del: true, search: true, refresh: true },
        {
            // edit options
            zIndex: 100,
            url: 'Essar_Users_Master/Edit',
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
            url: "Essar_Users_Master/Create",
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
            url: "Essar_Users_Master/Delete",
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
            caption: "Search Employee",
            sopt: ['cn']
        });
});