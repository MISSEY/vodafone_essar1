$(document).ready(function () {
    
   
    debugger;
    document.getElementById("change_req").addEventListener("click", function (e) {
        validate(e);
      

    });
    document.getElementById("level").onchange = function () {

        $.ajax({
            url: "Employee/Add2",
            type: "POST",
            dataType: "json",
            data: { Prefix: document.getElementById('level').value },
            success: function (data) {
                document.getElementById('Scheme').value = data["Plan"];
            }
        })


    }
    document.getElementById("level").onchange();
});
    function validate(e) {

        var errors = [];

        var Change_type = document.getElementById("Change_type");

        if (Change_type.value == 1) {
            document.getElementById("Scheme").disabled = false;
            document.getElementById("Scheme").style.background = "#fff";
        }
        else {
            document.getElementById("Scheme").disabled = true;
            document.getElementById("Scheme").style.background = "#E4E4E4";
        }
        if (Change_type.value == 2) {
            document.getElementById("SIM_No").disabled = false;
            document.getElementById("SIM_No").style.background = "#fff";
           

            document.getElementById("ReasonForsimReplacement").hidden = false;
        }
        else {
            document.getElementById("SIM_No").disabled = true;
            document.getElementById("SIM_No").style.background = "#E4E4E4";

            document.getElementById("ReasonForsimReplacement").hidden = true;
        }

        if (Change_type.value == 3) {
            document.getElementById("Mobile_No").disabled = false;
            document.getElementById("Mobile_No").style.background = "#fff";
        }
        else {
            document.getElementById("Mobile_No").disabled = true;
            document.getElementById("Mobile_No").style.background = "#E4E4E4";
        }



        var contact = document.getElementById("Mobile_No");
        allnumeric(contact)

        var email = document.getElementById("Email_Id");
        if (!checkEmail(email)) {
            errors[errors.length] = "You must enter a valid email address.";
        }

        var sub_total = document.getElementById("Sub_Total");
        if (!CheckDecimal(sub_total)) {
            errors[errors.length] = "You must enter numeric value";
        }
       
      
    
     
       
        document.getElementById("Scheme").onchange = function () {
            debugger;
            $.ajax({
                url: "Employee/Scheme",
                type: "POST",
                dataType: "json",
                data: { Prefix: document.getElementById('Scheme').value },
                success: function (data) {
                   document.getElementById('Limit').value = data;
                }
            })

        }
        document.getElementById("Scheme").onchange();



      
      
        
        

        // trigger when loading
      
        var sub_total = document.getElementById("Sub_Total");
        var GST_18 = document.getElementById("GST_18");
        GST_18.value = sub_total.value * 0.18

        var sub_total = document.getElementById("Sub_Total");
        var GST_18 = document.getElementById("GST_18");
        var total = document.getElementById("Total");
        total.value = parseInt(sub_total.value) + parseInt(GST_18.value);

        var limit = parseInt(document.getElementById("Limit").value);
        var Deduction = document.getElementById("Deduction");
        var total = parseInt(document.getElementById("Total").value);

        if (total > limit)
            Deduction.value = (total) - (limit)
        else
            Deduction.value = 0

        if (errors.length > 0) {
            reportErrors(errors);
            e.preventDefault();
        }
    }


    function allnumeric(inputtxt) {
        var numbers = /^([0-9]+$)?$/;
        if (inputtxt.value.match(numbers)) {
            //inputtxt.style.background = "#fff";
            return true;
        }
        else {
            inputtxt.style.background = "#fde0dc";
            //alert('Please input numeric characters only');
            return false;
        }
    }

    function CheckDecimal(inputtxt) {
        var decimal = /^([0-9]+(\.[0-9]+$)?$)?$/;
        if (inputtxt.value.match(decimal)) {
            //inputtxt.style.background = "#fff";
            return true;
        }
        else {
            inputtxt.style.background = "#fde0dc";
            return false;
        }
    }

    function checkEmail(email) {
        if (/^(\w+([\.-]?\w+)*@\w+([\.-]?\w+)*(\.\w{2,3})+$)?$/.test(email.value)) {
            //email.style.background = "#fff";
            return (true)
        }
        email.style.background = "#fde0dc";
        return (false)
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



                     
     
  