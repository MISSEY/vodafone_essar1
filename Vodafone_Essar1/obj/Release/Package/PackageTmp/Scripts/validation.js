$(document).ready(function () {
   
   
    var errors = [];
    var radioState = false;

    document.getElementById("Employees_Name2").onchange = function () {
        var s = document.getElementById("Employees_Name2").value;
        var a = s.split("|");
     
        document.getElementById("Employees_Name2").value = a[1];
        document.getElementById("SAP_ID2").value = a[0];
        document.getElementById("Email_Id2").value = a[2];
        
       document.getElementById("level2").value = a[4].trim();
        $('#level2').trigger('change');
        document.getElementById("Department2").value = a[3];
        //alert(a[4]);

    }
    document.getElementById("SAP_ID2").onchange = function () {
        var s = document.getElementById("SAP_ID2").value;
        var a = s.split("|");
        document.getElementById("SAP_ID2").value = a[0];
        document.getElementById("Employees_Name2").value = a[1];
        document.getElementById("Email_Id2").value = a[2];
        document.getElementById("level2").value = a[4].trim();
        $('#level2').trigger('change');
        document.getElementById("Department2").value = a[3];

    }
    var value;
    document.getElementById("level2").onchange = function () {
     
        $.ajax({
            url: "Employee/Add2",
            type: "POST",
            dataType: "json",
            data: { Prefix: document.getElementById('level2').value },
            success: function (data) {
              
                document.getElementById('Scheme2').value = data["Plan"];
                value=document.getElementById('Limit2').value = data["Limit"];
            }
        })

       
    }


    document.getElementById("Scheme2").onchange = function () {

        $.ajax({
            url: "Employee/Scheme",
            type: "POST",
            dataType: "json",
            data: { Prefix: document.getElementById('Scheme2').value },
            success: function (data) {
                document.getElementById('Limit2').value = data;
            }
        })

    }
 
    $('input[name="manual_limit"]').change(function () {

        if ($('#manual_limit').prop('checked')) {
            document.getElementById("Limit2").disabled = false;
            document.getElementById("Limit2").style.backgroundColor = "#fff";
        } else {
            document.getElementById("Limit2").value = value;
            document.getElementById("Limit2").disabled = true;
            document.getElementById("Limit2").style.backgroundColor = "#E4E4E4";
        }
    });
  
    $('input[name="careof_field"]').change(function () {

        if ($('#careof_field').prop('checked')) {
            document.getElementById("careofemployeeform").style.display = "block";
        } else {
            document.getElementById("careofemployeeform").style.display = "none";
        }
    });

    $("#careof_field").on('click', function () {
    
        if (radioState == false) {
            check();
            radioState = true;
        }
        else {
           
            uncheck();
            document.getElementById("careofemployeeform").style.display = "none";
            radioState = false;
        }
    });
    function check() {
        document.getElementById("careof_field").checked = true;
    }
    function uncheck() {
        document.getElementById("careof_field").checked = false;
    }

    document.getElementById("level2").onchange();
    document.getElementById("Email_Id2").onchange = function () {
        var s = document.getElementById("Email_Id2").value;
        var a = s.split("|");
        document.getElementById("Email_Id2").value = a[2];
        document.getElementById("SAP_ID2").value = a[0];
        document.getElementById("Employees_Name2").value = a[1];
       document.getElementById("level2").value = a[4].trim();
        document.getElementById("Department2").value = a[3];
        if (!checkEmail(a[3]    )) {
            errors[errors.length] = "You must enter a valid email address.";
        }

    }

    

   // document.getElementById("level2").addEventListener("change", cal_ded);

    //document.getElementById("Sub_Total2").addEventListener("change", cal_ded);



    if (errors.length > 0) {
        // reportErrors(errors);
        e.preventDefault();
    }
    });

function validate(e) {
   
}

function reportErrors(errors) {
    var msg = "There were some problems...\n";
    var numError;
    for (var i = 0; i < errors.length; i++) {
        numError = i + 1;
        msg += "\n" + numError + ". " + errors[i];
    }
    //alert(msg);
}



function checkEmail(email) {
    if (/^(\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$)?$/.test(email.value)) {
        email.style.background = "#fff";
        return (true)
    }
    email.style.background = "#fde0dc";
    return (false)
}

function CheckDecimal(inputtxt) {
    var decimal = /^([0-9]+(\.[0-9]+$)?$)/;
    if (inputtxt.value.match(decimal)) {
        inputtxt.style.background = "#fff";
        return true;
    }
    else {
        inputtxt.style.background = "#fde0dc";
        return false;
    }
}

function allnumeric(inputtxt) {
    var numbers = /^([0-9]+$)?$/;
    if (inputtxt.value.match(numbers)) {
        inputtxt.style.background = "#fff";
        return true;
    }
    else {
        inputtxt.style.background = "#fde0dc";
        //alert('Please input numeric characters only');
        return false;
    }
}

        
var today = new Date();

$('#add_new').submit(function () {
    document.getElementById("divLoading1").style.display = "block";
    $.ajax({
        url: 'Employee/Add',
        type: this.method,
        data: {
            
            Employees_Name: document.getElementById('To_Employee_Name').value + '(' + document.getElementById('Employees_Name2').value + ')',
            SAP_ID: document.getElementById('SAP_ID2').value,
            Levels: document.getElementById('level2').value,
            Mobile_No: document.getElementById('Mobile_No2').value,
            Limit: document.getElementById('Limit2').value,
            Sim_no: document.getElementById('SIM_No2').value,
            email_id: document.getElementById('Email_Id2').value,
            Billed_To: document.getElementById('Billed_To2').value,
            Department: document.getElementById('Department2').value,
            Month: today.getMonth(),
            Year: today.getFullYear(),
            status: '1',
            Scheme: document.getElementById('Scheme2').value
        },
        success: function (result) {
            if (result.success) {
                $('#AddDialog').dialog('close');
                document.getElementById("divLoading1").style.display = "none";
                window.location.reload();
                alert("Employee Registered");

            }
            else {
                alert(result);
                $('#AddDialog').dialog('close');
                document.getElementById("divLoading1").style.display = "none";
                window.location.reload();


            }
        }
    });
    return false;
});

