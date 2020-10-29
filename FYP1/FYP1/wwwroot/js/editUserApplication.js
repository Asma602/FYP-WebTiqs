
var attributes = [];
var formTitle;
var SetFormName;
var html_string = ' ';
var tableid;
var btn_string = '';
var saveTableName;
var formFields = {
    getTableName: ' ',
    fields: []

};

function getTableAttrs(idArg, tableIdArg) {
    saveTableName = $(idArg).val();
    console.log(saveTableName);
    tableid = tableIdArg;
    $.ajax({
        url: "/EditUserApplication/getTableAttributes",
        type: "POST",
        data: {
            nameTable: $(idArg).val(),
        },
        success: function (data) {
            $("#getFormAttrs").html(data);
        },
        error: function (err) {
            console.log(err);
        }

    });
    document.getElementById("titleOfForm").innerHTML = $(idArg).val();
    formTitle = $(idArg).val();
}
function getApplicationForm(id){
      $.ajax({
            url: "/EditUserApplication/getApplicationFormView",
            type: "POST",
            data: {
              formId:id
            },
            success: function (data) {
                 $("#getFormAttrs").html(data.formString);
                  $("#titleOfForm").html(data.formTitle);
                  $("#deleteApplicationForm").attr('id', data.formId);
            
                 document.getElementById(data.formId).style.visibility = 'visible';
            },
            error: function (err) {
                console.error();
            }
        });
}
function deleteApplicationForm(id) {

    if (confirm("Are you sure you want to delete this Form?")) {

        $.ajax({
            url: "/EditUserApplication/deleteApplicationForm",
            type: "POST",
            data: {
                formId: id
            },
            success: function (data) {
                $("#getFormAttrs").html('');
                $("#titleOfForm").html('');
                $("#deleteApplicationForm").attr('id', '');
                $("#formsHere").html(data);
                document.getElementById(id).style.visibility = 'hidden';
                $("#" + id).attr('id', "deleteApplicationForm");

            },
            error: function (err) {

            }
        });
    }
    else {
        return;
    }
     
}
function getForm() {


    var attrDataType = [];
    var attrDesc = [];
    var attrReq = [];
    $.each($("input[type=checkbox]:checked"), function () {
        attrDataType.push($(this).attr("dataType"));
        attrDesc.push($(this).attr("desc"));
        attrReq.push($(this).attr("req"));
    });


    console.log(attrDataType);
    console.log(attrDesc);
    console.log(attrReq);
    if (attrDataType.length > 0) {
        var setMyFormId = ($("#SetFormTitle").val());
        document.getElementById("formTitle").innerHTML = ($("#SetFormTitle").val()).toUpperCase();
        html_string = '<form  style="border:1px solid grey;" class="formDiv  p-2" id="' + setMyFormId + '" >';

        formFields.getTableName = setMyFormId;
        for (var i = 0; i < attrDataType.length; i++) {
            switch (attrDataType[i]) {
                 case "int":
                    html_string += '<div class="form-group"> <label class="labelSize" id="label'+attrDesc[i]+'">' + attrDesc[i].toUpperCase() + ' (i.e Choose related table record)</label>';
                    if (attrReq[i] == 1) {
                        html_string += " " + "(Required)";
                        attrReq[i] = "true";
                    } else {
                        attrReq[i] = "false";
                    }
                    html_string += '<input class="form-control" id="' + attrDesc[i] + '" type="number"' + 'placeholder="' + attrDesc[i] + '"' + 'required="' + attrReq[i] + '"  />' + '<br/> </div>';
                    formFields.fields[i] = attrDesc[i];
                    break;
                 case "string":

                    html_string += '<div class="form-group"> <label class="labelSize" id="label'+attrDesc[i]+'" >' + attrDesc[i].toUpperCase() + '</label>';
                    if (attrReq[i] == 1) {
                        html_string += " " + " <span class='labelSize'>(Required)</span>";
                        attrReq[i] = "true";
                    } else {
                        attrReq[i] = "false";
                    }
                    html_string += '<input class="form-control" id="' + attrDesc[i] + '"  type="text +"  placeholder="' + attrDesc[i] + '"  required="' + attrReq[i] + '"/>' + '<br/> </div>';
                    formFields.fields[i] = attrDesc[i];
                    break;
                case "select":
                    var tableName = attrDesc[i].slice(0, -2);
                    html_string += '<div class="form-group" id="selectId"> <label class="labelSize" >' + tableName + ' (i.e Choose related table record)</label>';
                    if (attrReq[i] == 1) {
                        html_string += " " + "<span class='labelSize'>(Required)</span>";
                        attrReq[i] = "true";
                    } else {
                        attrReq[i] = "false";
                    }
                    
                    html_string += '<select class="form-control" id="' + attrDesc[i] + '"required="' + attrReq[i] + '"  ><option value="' + tableName + '" hidden selected></option></select>' + '<br/> </div>';
                    formFields.fields[i] = attrDesc[i];
                    break;
                case "varchar":

                    html_string += '<div class="form-group"> <label class="labelSize" id="label' + attrDesc[i] + '" >' + attrDesc[i].toUpperCase() + '</label>';
                    if (attrReq[i] == 1) {
                        html_string += " " + " <span class='labelSize'>(Required)</span>";
                        attrReq[i] = "true";
                    } else {
                        attrReq[i] = "false";
                    }
                    html_string += '<input class="form-control" id="' + attrDesc[i] + '"  type="' + attrDataType[i] + '"  placeholder="' + attrDesc[i] + '"  required="' + attrReq[i] + '"/>' + '<br/> </div>';
                    formFields.fields[i] = attrDesc[i];
                    break;
                case "boolean":
                    html_string += '<div class="form-group"> ';
                    if (attrReq[i] == 1) {
                        html_string += " " + "<span class='labelSize'>(Required)</span>";
                        attrReq[i] = "true";
                    } else {
                        attrReq[i] = "false";
                    }
                    html_string += '<input id="' + attrDesc[i] + '" type="checkbox" ' + "  required=" + attrReq[i] + '>' + ' <span class="ml-2">' + attrDesc[i] + '</span > ' + '<br/> </div>';
                    formFields.fields[i] = attrDesc[i];
                    break;
                case "number":
                    html_string += '<div class="form-group"> <label class="labelSize" id="label' + attrDesc[i] + '">' + attrDesc[i].toUpperCase() + '</label>';
                    if (attrReq[i] == 1) {
                        html_string += " " + "(Required)";
                        attrReq[i] = "true";
                    } else {
                        attrReq[i] = "false";
                    }
                    html_string += '<input class="form-control" id="' + attrDesc[i] + '" type="number"' + 'placeholder="' + attrDesc[i] + '"' + 'required="' + attrReq[i] + '"  />' + '<br/> </div>';
                    formFields.fields[i] = attrDesc[i];
                    break;
                case "button":
                    html_string += '<div class="form-group">';
                    if (attrReq[i] == 1) {
                        html_string += " " + "(Required)";
                        attrReq[i] = "true";
                    } else {
                        attrReq[i] = "false";
                    }
                    html_string += '<button class="form-control id="' + attrDesc[i] + '" btn btn-outline-primary" ' + 'value="' + attrDesc[i] + '"' + 'required="' + attrReq[i] + '"  />' + '<br/> </div>';
                    formFields.fields[i] = attrDesc[i];
                    break;
                case "email":
                    html_string += '<div class="form-group"> <label class="labelSize"  id="label' + attrDesc[i] + '">' + attrDesc[i].toUpperCase() + '</label>';
                    if (attrReq[i] == 1) {
                        html_string += " " + "(Required)";
                        attrReq[i] = "true";
                    } else {
                        attrReq[i] = "false";
                    }
                    html_string += '<input class="form-control" id="' + attrDesc[i] + '" type="email"' + 'placeholder="' + attrDesc[i] + '"' + 'required="' + attrReq[i] + '"  />' + '<br/> </div>';
                    formFields.fields[i] = attrDesc[i];
                    break;
                case "password":
                    html_string += '<div class="form-group"> <label class="labelSize" id="label' + attrDesc[i] + '">' + attrDesc[i].toUpperCase() + '</label>';
                    if (attrReq[i] == 1) {
                        html_string += " " + "(Required)";
                        attrReq[i] = "true";
                    } else {
                        attrReq[i] = "false";
                    }
                    html_string += '<input class="form-control" id="' + attrDesc[i] + '" type="password"' + 'placeholder="' + attrDesc[i] + '"' + 'required="' + attrReq[i] + '"  />' + '<br/> </div>';
                    formFields.fields[i] = attrDesc[i];
                    break;
                case "file":
                    html_string += '<div class="form-group"> <label class="labelSize" id="label' + attrDesc[i] + '">' + attrDesc[i].toUpperCase() + '</label>';
                    if (attrReq[i] == 1) {
                        html_string += " " + "(Required)";
                        attrReq[i] = "true";
                    } else {
                        attrReq[i] = "false";
                    }
                    html_string += '<input class="form-control"  id="' + attrDesc[i] + '" type="file"' + 'placeholder="' + attrDesc[i] + '"' + 'required="' + attrReq[i] + '"  />' + '<br/> </div>';
                    formFields.fields[i] = attrDesc[i];
                    break;
                case "url":
                    html_string += '<div class="form-group"> <label class="labelSize"  id="label' + attrDesc[i] + '">' + attrDesc[i].toUpperCase() + '</label>';
                    if (attrReq[i] == 1) {
                        html_string += " " + "(Required)";
                        attrReq[i] = "true";
                    } else {
                        attrReq[i] = "false";
                    }
                    html_string += '<input class="form-control" id="' + attrDesc[i] + '" type="number"' + 'placeholder="e.g https://www.xyz.com"' + 'required="' + attrReq[i] + '"  />' + '<br/> </div>';
                    formFields.fields[i] = attrDesc[i];
                    break;
                case "search":
                    html_string += '<div class="form-group"> <label  class="labelSize" id="label' + attrDesc[i] + '">' + attrDesc[i].toUpperCase() + '</label>';
                    if (attrReq[i] == 1) {
                        html_string += " " + "(Required)";
                        attrReq[i] = "true";
                    } else {
                        attrReq[i] = "false";
                    }
                    html_string += '<input class="form-control"  id="' + attrDesc[i] + '" type="search"' + 'placeholder="' + attrDesc[i] + '"' + 'required="' + attrReq[i] + '"  />' + '<br/> </div>';
                    formFields.fields[i] = attrDesc[i];
                    break;
                case "date":
                    html_string += '<div class="form-group"> <label class="labelSize"  id="label' + attrDesc[i] + '">' + attrDesc[i].toUpperCase() + '</label>';
                    if (attrReq[i] == 1) {
                        html_string += " " + "(Required)";
                        attrReq[i] = "true";
                    } else {
                        attrReq[i] = "false";
                    }
                    html_string += '<input class="form-control" id="' + attrDesc[i] + '" type="date"' + 'placeholder="' + attrDesc[i] + '"' + 'required="' + attrReq[i] + '"  />' + '<br/> </div>';
                    formFields.fields[i] = attrDesc[i];
                    break;

                case "textarea":
                    html_string += '<div class="form-group"> <label class="labelSize" id="label' + attrDesc[i] + '">' + attrDesc[i].toUpperCase() + '</label>';
                    if (attrReq[i] == 1) {
                        html_string += " " + "(Required)";
                        attrReq[i] = "true";
                    } else {
                        attrReq[i] = "false";
                    }
                    html_string += '<textarea class="form-control" id="' + attrDesc[i] + '" type="date"' + '"' + 'required="' + attrReq[i] + '"  /> ' + '<br/> </div>';
                    formFields.fields[i] = attrDesc[i];
                    break;
                
            }

        }

        html_string += '<button type="button" class="btn bgColor" data-dismiss="modal"  id="setBtn" onclick="saveFormVals()">Save</button>';

        html_string += "</form>";
        $('#myform').html(html_string);

        //btn_string += '<form><button type="button" class="btn bgColor"  onclick="addFormToList();">Save</button></form>';

        //$("#saveBtn").html(btn_string);
        //btn_string = " ";

        //     styleForm();

    }
    else {
        alert("Please select appropriate number of attributes!");
    }
    $("#myform *").attr("disabled", "disabled").off('click');
}

var formList = [];
var x = 1;
function addFormToList() {
    Set_html_string = $("#myform").html();
    var formObject = {

        tableId: tableid,
        formCodeStr: Set_html_string,
        fname: $("#SetFormTitle").val(),
        tname: saveTableName,
        dbname: $("#saveDbName").val(),
    };
    formList.push(formObject);
    console.log(formList);
    $.ajax({
        url: "/EditUserApplication/addForm",
        type: "POST",
        data: {

            idOfTable: formObject.tableId,
            nameOfForm: formObject.fname,
            formString: formObject.formCodeStr,
            tableName: formObject.tname,
            nameOfDb: formObject.dbname,
        }
    });

    saveFormFields();
    location.reload();

}

function styleForm() {
    $.ajax({
        url: "/EditUserApplication/styleForm",
        type: "GET",

        success: function (data) {
            $("#styleDiv").html(data);
        }
    });


}
function applyStyles() {
    $("#getFormAttrs").html("");
    $("#titleOfForm").text("");
    $("#myform").html($("#popUpForm").html());
}

function saveFormFields() {
    console.log(formFields);
}


