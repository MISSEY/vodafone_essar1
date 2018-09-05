$(document).ready(function () {
   
  
    //document.getElementById("add_new").addEventListener("click", function (e) {
    //    validate(e);

    
    var errors = [];
   
    document.getElementById("divLoading1").style.display = "block";

    var contact = document.getElementById("Mobile_No3");
    allnumeric(contact)

    document.getElementById("Email_Id3").addEventListener("change", function (e) {
        //alert();
        var email = document.getElementById("Email_Id3");
        if (!checkEmail(email)) {
            errors[errors.length] = "You must enter a valid email address.";
        }
    });



    var value;
    document.getElementById("level3").onchange = function () {
        
        $.ajax({
            url: "Employee/Add2",
            type: "POST",
            dataType: "json",
            data: { Prefix: document.getElementById('level3').value },
            success: function (data) {
              
                document.getElementById('Scheme3').value = data["Plan"];
                value=document.getElementById('Limit3').value = data["Limit"];
            }
        })

       
    }
 
    $('input[name="manual_limit3"]').change(function () {

        if ($('#manual_limit3').prop('checked')) {
            document.getElementById("Limit3").disabled = false;
            document.getElementById("Limit3").style.backgroundColor = "#fff";
        } else {
            document.getElementById("Limit3").value = value;
            document.getElementById("Limit3").disabled = true;
            document.getElementById("Limit3").style.backgroundColor = "#E4E4E4";
        }
    });

    document.getElementById("Scheme3").onchange = function () {
       
        $.ajax({
            url: "Employee/Scheme",
            type: "POST",
            dataType: "json",
            data: { Prefix: document.getElementById('Scheme3').value },
            success: function (data) {
             
                document.getElementById('Limit3').value = data;
            }
        })

    }
    document.getElementById("level3").onchange();


    if (errors.length > 0) {
        // reportErrors(errors);
        e.preventDefault();
    }
    });



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

        
     


$('#care_of').submit(function () {

    document.getElementById("divLoading1").style.display = "block";
    $.ajax({
        url: 'Employee/Add',
        type: this.method,
        data: {

            Employees_Name: document.getElementById('Employees_Name3').value,
            SAP_ID: document.getElementById('SAP_ID3').value,
            Levels: document.getElementById('level3').value,
            Mobile_No: document.getElementById('Mobile_No3').value,
            Limit: document.getElementById('Limit3').value,
            Sim_no: document.getElementById('SIM_No3').value,
            email_id: document.getElementById('Email_Id3').value,
            Billed_To: document.getElementById('Billed_To3').value,
            Department: document.getElementById('Department3').value,
            Month: today.getMonth(),
            Year: today.getFullYear(),
            status: '1',
            Scheme: document.getElementById('Scheme3').value
        },
        success: function (result) {
            if (result.success) {
                $('#AddDialog').dialog('close');
                document.getElementById("divLoading1").style.display = "none";
                window.location.reload();
                alert("Employee Registered Successfully");
                

            }
            else {
                alert(result);
                $('#AddDialog').dialog('close');
                window.location.reload();
                  

            }
        }
    });
    return false;
});
