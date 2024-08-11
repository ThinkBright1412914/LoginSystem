$(document).ready(function () {
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

