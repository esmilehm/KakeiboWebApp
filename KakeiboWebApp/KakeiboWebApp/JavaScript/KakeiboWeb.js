<!-- 入力制限 数字のみ -->
function CheckNum()
{
if (((event.keyCode < 48) || (event.keyCode > 57)) && (event.keyCode != 45) && (event.keyCode!=46)){
window.event.returnValue = false;
}
}