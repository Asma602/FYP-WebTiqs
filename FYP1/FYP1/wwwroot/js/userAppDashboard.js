


  function getAllData(id) {
        //console.log("getAllldata");
        var tableName = $("#table" + id).val();

        $.ajax({
        url: '/Form/getView',
        type: 'POST',
        dataType: 'json',
        data: {
            getFormId: id,
            tableName: tableName
        },
        success: function (data) {
            $("#fView").html(data[0]);
            $("#heading").html(data[1]);
            getRecord(id);
        },
        error: function (err) {
        }
        });
  }
  function getRecord(id) {
        var tableName = $("#table" + id).val();
        //$("#showTableName").text(tableName);
        var formid = $("#heading").html();
        var formControls = document.getElementById(formid).elements;
        var ids = [];

        for (var i = 0; i < formControls.length; i++) {
            if (formControls[i].nodeName === "INPUT" || formControls[i].nodeName === "SELECT") {
                ids[i] = document.getElementById(formid).elements[i].id;
            }
        }
        var formObject = {
            attrsid: ids,
        };
        $.ajax({
        url: '/Form/getRecord',
        type: 'POST',
        data: {
            cols: formObject.attrsid,
            tableName: tableName,
            dbName: $("#dbName").val()
        },
        success: function (data) {
            console.log(data);
            $("#formView").html(data);
 $("#editFormRecord").html('');
            $('#tbl-appData').DataTable(
                {
                    language: {
                        searchPlaceholder: "Search",
                        search: "",
                    },
                }
            );
        },
        error: function (err) {
        }
        });
  }
  function getFormView(idArg) {
      var tableName = $("#table" + idArg).val();
      $.ajax({
        url: '/Form/getView',
        type: 'POST',
        dataType: 'json',
        data: {
            getFormId: idArg,
              tableName: tableName
        },
        success: function (data) {
            $("#formView").html(data[0]),
 $("#editFormRecord").html('');
            $("#formHeading").html(data[1]),
            $("#tabName").html(data[2]),
            $("#formView *").removeAttr("disabled").off('click');
            getForeignKeyData();
        },
        error: function (err) {
        }
      });
  }
  function getForeignKeyData() {
        var tablenames;
        var formid = $("#formHeading").html();
        var formControls = document.getElementById(formid).elements;
        for (var i = 0; i < formControls.length; i++) {
            if (formControls[i].nodeName === "SELECT") {
                tablenames = formControls[i].value;
                getDataFromTable(tablenames);
            }
        }
  }
    function getDataFromTable(tablenames) {
        $.ajax({
            type: "POST",
            url: "/Form/getForeignKey",
            data: {
                parentTable: tablenames
            },
            success: function (text) {
                $("#" + tablenames + "id").html(text);
            }
        });
}
    function saveFormVals() {
        var formid = $("#formHeading").html();
        var formControls = document.getElementById(formid).elements;
        var inputs = [];
        var ids = [];
        
        for (var i = 0; i < formControls.length; i++) {
            if (formControls[i].nodeName === "INPUT") {
                ids[i] = document.getElementById(formid).elements[i].id;
                inputs[i] = formControls[i].value;
            }
            else if (formControls[i].nodeName === "SELECT") {
                ids[i] = document.getElementById(formid).elements[i].id;
                inputs[i] = formControls[i].value;
            }
        }

        var formObject = {
            formname: $("#formHeading").html(),
            attrsid: ids,
            attrsvals: inputs
        };
     
        $.ajax({
            type: "POST",
            url: "/Form/saveFormVals",
            dataType: "JSON",
            data: {
                fname: formObject.formname,
                cols: formObject.attrsid,
                attrVals: formObject.attrsvals,
            },

            success: function (text) {
                alert("Record Added Successfully");
                document.getElementById(formid).reset();
                $("#testing").html(text);
                $('#tbl-appData').DataTable(
                    {
                        language: {
                            searchPlaceholder: "Search",
                            search: "",
                        },
                    }
                );
            }
        });
        
    }

    function deleteAppDataRecord(id) {
        var tableName = $("#appDataTableName").html();
        var dbName = $("#appDataDbName").html();
        if (confirm("Data in related tables will also be deleted.Are you sure you want to delete this record?")) {
            $.ajax({
                url: '/Form/deleteAppDataRecord',
                type: 'POST',
                data: {
                    id: id,
                    tableName: tableName,
                    dbName: dbName
                },
                success: function (data) {
                    $("#formView").html(data);
                    $("#editFormRecord").html('');
                    $('#tbl-appData').DataTable(
                        {
                            language: {
                                searchPlaceholder: "Search",
                                search: "",
                            },
                        }
                    );
                },
                error: function (err) {
                }
            });
        }
        else {
            return;
        }
       
    }
function editAppDataRecord(id){
 $.ajax({
            url: '/Form/editAppDataRecord',
            type: 'POST',
            data: {
                id: id
            },
            success: function (data) {
                console.log(data),
                $("#formView").html(''),
               $("#editFormRecord").html(data[0]),
            $("#formHeading").html(data[1]),
            $("#tabName").html(data[2]),
          

            $("#editFormRecord *").removeAttr("disabled").off('click');
            var button = document.createElement('button');
             var t = document.createTextNode("Update");
                button.appendChild(t);
        // SET INPUT ATTRIBUTE 'type' AND 'value'.
             button.setAttribute('type', 'button');
             button.setAttribute('value', 'Update');
             button.setAttribute('id',data[2]);
        // ADD THE BUTTON's 'onclick' EVENT.
             button.setAttribute('onclick', 'updateAppRecord(this.id)');
            document.getElementById(data[1]).appendChild(button);
             var elem = document.getElementById('setBtn');
             elem.parentNode.removeChild(elem);
            $("#"+data[2]).addClass("UpdateButtonOfRecord");
            getForeignKeyData();
                
            },
            error: function (err) {
            }
        });

}
 function updateAppRecord(id) {
console.log(id);
 var formid = $("#formHeading").html();
        var formControls = document.getElementById(formid).elements;
        var inputs = [];
        var ids = [];
        for (var i = 0; i < formControls.length; i++) {
            if (formControls[i].nodeName === "INPUT") {
                ids[i] = document.getElementById(formid).elements[i].id;
                inputs[i] = formControls[i].value;
            }
            if (formControls[i].nodeName === "SELECT"){
                ids[i] = document.getElementById(formid).elements[i].id;
                inputs[i] = formControls[i].value;
            }
        }
        var formObject = {
            attrsid: ids,
            attrsvals: inputs
        };
console.log(formObject);
        $.ajax({
            type: "POST",
            url: "/Form/updateAppRecord",
            data: {
             
               cols: formObject.attrsid,
                attrVals: formObject.attrsvals,
            },
            success: function (data) {
                $("#formView").html(data);
                $("#editFormRecord").html('');
                $('#tbl-appData').DataTable(
                {
                    language: {
                        searchPlaceholder: "Search",
                        search: "",
                    },
                }
                );
            },
            error: function (err) {
                console.log(err);
            }
        });
}
   
   /* function editAppDataRecord(id) {
        var tableName = $("#appDataTableName").html();
        var dbName = $("#appDataDbName").html();
        $.ajax({
            url: '/Form/editAppDataRecord',
            type: 'POST',
            data: {
                id: id,
                tableName: tableName,
                dbName: dbName
            },
            success: function (data) {
                console.log(data);
               $("#formView").html(data);
                $('#tbl-appData').DataTable(
                    {
                        language: {
                            searchPlaceholder: "Search",
                            search: "",
                        },
                    }
                );
                
            },
            error: function (err) {
            }
        });
    }
*/