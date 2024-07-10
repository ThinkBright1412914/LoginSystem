
function AvoidSpace(event) {
    var value = event ? event.which : window.event.keyCode;
    if (value === 32) return false;
}

$('#showPassword').click(function () {
    if ('password' == $('#Password').attr('type')) {
        $('#Password').prop('type', 'text');
    } else {
        $('#Password').prop('type', 'password');
    }
})

$('#showPassword2').click(function () {
    if ('password' == $('#ConfirmPassword').attr('type')) {
        $('#ConfirmPassword').prop('type', 'text');
    } else {
        $('#ConfirmPassword').prop('type', 'password');
    }
})
