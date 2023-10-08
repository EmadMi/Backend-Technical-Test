// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.

function InitializeDateTimePicker(datePickerList) {
    $(datePickerList).each(function () {
        let TextTarget = $(this).data("dp-text");
        let ValueTarget = $(this).data("dp-value");
        let TimePickerState = $(this).data("dp-timepicker");
        let ToDateState = $(this).data("dp-todate");
        let FromDateState = $(this).data("dp-fromdate");
        let GroupIdValue = $(this).data("dp-groupid");
        let SelectedDate = $(ValueTarget).val();
        //
        new mds.MdsPersianDateTimePicker(this, {
            targetTextSelector: TextTarget,
            targetDateSelector: ValueTarget,
            isGregorian: true,
            enableTimePicker: TimePickerState,
            toDate: ToDateState,
            fromDate: FromDateState,
            groupId: GroupIdValue
        });
    });
}