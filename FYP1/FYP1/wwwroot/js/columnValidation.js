 

function  fieldNameCheck(){
         var regex = /^[A-Za-z_]{1,64}$/;
            var reg=new RegExp(regex);
                var fieldName=$("#fieldName").val();
                if(reg.test(fieldName)){
                    $("#fieldName").css("border-color","#D8D8D8");
                     $("#invalidFieldName").hide();
                     return 1;
                }
                else{
                     $("#fieldName").css("border-color","red");
                        $("#invalidFieldName").show();
                      return 0;
                  }
    }
     function  dataTypeCheck(){
             var flag=1;
                var dataType=$("#dataType").val();
                if(dataType!=null){
                    $("#dataType").css("border-color","#D8D8D8");
                     $("#invalidDataType").hide();
                     flag=1;
                }
                else{
                     $("#dataType").css("border-color","red");
                        $("#invalidDataType").show();
                      flag=0;
                  }
return flag;
        }
    function  requiredCheck(){
         var required=$("#required").val();
                if(required!=null){
                    $("#required").css("border-color","#D8D8D8");
                     $("#invalidRequired").hide();
                     return  1;
                }
                else{
                     $("#required").css("border-color","red");
                        $("#invalidRequired").show();
                      return  0;
                  }
    }
    function  minLengthCheck(){
         var minLength=$("#minLength").val();
                if(minLength>0){
                    $("#minLength").css("border-color","#D8D8D8");
                     $("#invalidMinLength").hide();
                     return  1;
                }
                else{
                     $("#minLength").css("border-color","red");
                        $("#invalidMinLength").show();
                      return  0;
                  }
    }
    function maxLengthCheck(){
        var minLength=$("#minLength").val();
        var maxLength=$("#maxLength").val();
                if(maxLength>minLength){
                    $("#maxLength").css("border-color","#D8D8D8");
                     $("#invalidMaxLength").hide();
                     return  1;
                }
                else{
                     $("#maxLength").css("border-color","red");
                        $("#invalidMaxLength").show();
                       return 0;
                  }

    }
  
   