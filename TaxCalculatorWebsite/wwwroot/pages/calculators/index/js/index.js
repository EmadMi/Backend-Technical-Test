function AddNewPassDateTime() {
    let PassDateTimeCtrl = $(document).find("input[id^='PassDateTimeList_'");
    let CurrentIndex = PassDateTimeCtrl.length;
    //
    let ColorClass = (CurrentIndex % 2 == 0) ? "bg-light" : "bg-secondary bg-opacity-10";
    //
    let RawTemplate = $("#dvPassDateTimeTemplate").clone();
    //
    let GeneratedInputId = `PassDateTimeList_${CurrentIndex}__PassDateTime`;
    let GeneratedInputName = `PassDateTimeList[${CurrentIndex}].PassDateTime`;
    //
    $(RawTemplate).find("input[id='#GeneratedInputId']").attr("id", GeneratedInputId).attr("name", GeneratedInputName);
    $(RawTemplate).find("label[for='#GeneratedInputId']").attr("for", "#" + GeneratedInputId);
    $(RawTemplate).find("span.text-danger").attr("data-valmsg-for", GeneratedInputName);
    //
    $(RawTemplate).removeClass("d-none").addClass(ColorClass).removeAttr("id");
    //
    $("#dvAddPassDateTime").before(RawTemplate);
}