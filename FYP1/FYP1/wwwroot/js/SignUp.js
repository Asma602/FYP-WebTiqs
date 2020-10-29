

function validate() {
    var pattern = /^[a-zA-Z_0-9@\!#\$\^%&*()+=\-[]\\\';,\.\/\{\}\|\":<>\? ]+$/;
    var pwdValue = $("#defaultRegisterFormPassword").val();
    if (!(pattern.test(pwdValue))){
        $("#errorMsg").html("Incorrect Password Format!");
    }
}