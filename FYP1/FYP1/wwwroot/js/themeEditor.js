

var formDiv = document.querySelector(".formDiv");

var formHeading = document.querySelector("#formTitle");
const form = document.querySelector('.formDiv');
var bg = document.querySelector("#setBgColor");

var setBtn = document.querySelector("#setBtn");

  var picker = new Picker(bg);
  picker.onChange = function (color) {
  formDiv.style.background = color.rgbaString;
  console.log(formDiv.style.background);
}

var txtColor = document.querySelector("#setTextColor");
var picker = new Picker(txtColor);
picker.onChange = function (color) {
    formDiv.style.color = color.rgbaString;
    console.log(formHeading);
    formHeading.style.color = color.rgbaString;
}

var btnColor = document.querySelector("#setBtnColor");
var picker = new Picker(btnColor);
picker.onChange = function (color) {
    setBtn.style.backgroundColor = color.rgbaString;
}

var btnTxtColor = document.querySelector("#setBtnTextColor");
var picker = new Picker(btnTxtColor);
picker.onChange = function (color) {
    setBtn.style.color = color.rgbaString;
}


// style divs

function setFontFamily() {
    form.style.setProperty('font-family', $("#setFontFamily").val());
    formHeading.style.setProperty('font-family', $("#setFontFamily").val());
}
function setFontSize() {
    form.style.setProperty('font-size', $("#setFontSize").val() + "px");
    $("#getFontSize").html($("#setFontSize").val());
    formHeading.style.setProperty('font-size', $("#setFontSize").val() + "px");
}
function setLetterSpacing() {
    form.style.setProperty('letter-spacing', $("#setLetterSp").val() + "px");
    $("#getLetterSp").html($("#setLetterSp").val());
}
function setWordSpacing() {
    form.style.setProperty('word-spacing', $("#setWordSp").val() + "px");
    $("#getWordSp").html($("#setWordSp").val());
}
function setBgColor() {

    //	form.style.setProperty('--set-bg-color',$("#setBgColor").val());
}
//function setTextColor() {
//    form.style.setProperty('--set-text-color', $("#setTextColor").val());
//}
function setFontWeight() {
    var selectedOption = $("#setFontWeight option:selected").val();
    form.style.setProperty('font-weight', selectedOption);
}
function setTextDecoration() {
    form.style.setProperty('text-decoration', $("#setTextDecor").val());
}
function setFontStyle() {
    form.style.setProperty('font-style', $("#setFontStyle").val());
}
function setTextPosition() {
    form.style.setProperty('text-position', $("#setTextPosition").val());
    console.log($("#setTextPosition").val());
}
function setBtnColor() {
    $("#setBtn").addClass($("#setBtnColor").val());
    console.log($("#setBtnColor").val());
}
function setBtnOutline() {
    $("#setBtn").addClass($("#setBtnOutline").val());
}
function setBtnSize() {
    $("#setBtn").addClass($("#setBtnSize").val());

}
