$(document).ready(function () {
    var date = new Date();
    var currentDate = (date.getMonth() + 1).toString().padStart(2, '0') + '/' +
        date.getDate().toString().padStart(2, '0') + '/' +
        date.getFullYear();

    $('#releaseDate').daterangepicker({
        singleDatePicker: true,
        showDropdowns: true,
        "minDate": currentDate,
    });

    $('#autowidth').lightSlider({
        item : 3,
        autoWidth: false,
        auto :true,
        loop: true,
        pager: false,
        onSliderLoad: function () {
            $('#autowidth').removeClass('cs-hidden');
        }
    })
})
