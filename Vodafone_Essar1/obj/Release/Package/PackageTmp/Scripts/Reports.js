
function BindTable() {
    debugger;
    $.ajax({

        type: "POST",
        dataType: "json",

        url: "Reports/GetReports",
        data: {
            from: document.getElementById('from').value,
            to: document.getElementById('to').value,
        },

        success: function (data1) {
            Bind(data1);
            //document.getElementById("divLoading").style.display = "none";
        }
    });
}

function Bind(data1) {
    debugger;
    $('#Employeetable').dataTable().fnDestroy();
    var datatableVariable = $('#Employeetable').DataTable({
        data: data1,

        "sScrollX": true,
        "sScrollY": 400,
        "bScrollCollapse": true,
        dom: 'Bfrtip',
        buttons: [
           'excel', 'print'
        ],
        columns: [
            {
                data: 'Date'
            },

            { 'data': 'Employees_Name' },
            { 'data': 'SAP_ID' },
            { 'data': 'Levels' },
            { 'data': 'Mobile_no' },
            {
                'data': 'Sim_no',
            },
            { 'data': 'Reason' },        
            { 'data': 'Department' }
            ]
    });
}


$(document).ready(function () {
    
  
        
    var Month,sim_no;
    document.getElementById('from').value = moment().format('DD/MM/YYYY');
    document.getElementById('to').value = moment().format('DD/MM/YYYY');
  
    debugger;
    BindTable();

    var dateFormat = "dd/mm/yy",
          from = $("#from")
            .datepicker({
                defaultDate: "+1w",
                changeMonth: true,
                changeYear: true,
                dateFormat: "dd/mm/yy",
                numberOfMonths: 1
            })
            .on("change", function () {
                to.datepicker("option", "minDate", getDate(this));
            }),
          to = $("#to").datepicker({
              defaultDate: "+1w",
              changeMonth: true,
              dateFormat: "dd/mm/yy",
              changeYear: true,
              numberOfMonths: 1
          })
          .on("change", function () {
              from.datepicker("option", "maxDate", getDate(this));
          });

    function getDate(element) {
        var date;
        try {
            date = $.datepicker.parseDate(dateFormat, element.value);
        } catch (error) {
            date = null;
        }

        return date;
    }
        
 
  
   
    $("#MonthYear").click(function () {
        document.getElementById("divLoading").style.display = "block";

        BindTable();
           
    });

   
        
});


