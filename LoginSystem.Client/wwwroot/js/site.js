$('#addGenre').click(function () {
    $('#genreModal').modal('show');
});

$('#saveGenre').off('click').on('click', function () {
    var name = $('#Name').val().trim();
    if (name === '') {
        toastr.error('Please enter genre!');
    } else {
        $.ajax({
            url: '/Genre/Create',
            method: 'POST',
            data: {name },
            dataType: 'json',
            success: function (result) {
                if (result.message) {
                    toastr.success(result.message);
                    btnModalClose();
                    location.reload();
                } else {
                    toastr.error('Failed to create genre');
                }
            },
            error: function (xhr, status, error) {
                toastr.error('An error occurred: ' + xhr.responseText);
            }
        });
    }
});

function btnModalClose() {
    $('#genreModal').modal('hide');
}

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

