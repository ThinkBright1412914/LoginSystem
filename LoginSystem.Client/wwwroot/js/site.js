$(document).ready(function () {
    $('.dropdownMovie').select2();
})

function ValidateInput() {
    if (document.getElementById("uploadBox").value == "") {
        Swal.fire({
            icon: 'error',
            title: 'Oops...',
            text: 'Please upload an Image!',
        });
        return false;
    }
    return true;
}


//For ShowTime
$('#addShowTime').click(function () {
    $('#showTimeModal').modal('show');
});

$('#saveTime').off('click').on('click', function () {
    var time = $('#time').val();
    if (time === '') {
        toastr.error('Please enter time!');
    }
    else
    {
        var timeParts = time.split(':');
        var hours = parseInt(timeParts[0], 10);
        var minutes = timeParts[1];
        var ampm = hours >= 12 ? 'PM' : 'AM';
        hours = hours % 12;
        hours = hours ? hours : 12;
        var formattedTime = hours + ':' + minutes + " " +  ampm;
        $.ajax({
            url: '/ShowTime/Create',
            method: 'POST',
            data: { time: formattedTime },
            dataType: 'json',
            success: function (result) {
                if (result.message) {
                    toastr.success(result.message);
                    btnModalClose();
                    location.reload();
                } else {
                    toastr.error('Failed to create time slots');
                }
            },
            error: function (xhr, status, error) {
                toastr.error('An error occurred: ' + xhr.responseText);
            }
        });
    }
});

function btnShowTimeClose() {
    $('#showTimeModal').modal('hide');
}


//For Industry
$('#addIndustry').click(function () {
    $('#industryModal').modal('show');
});

$('#saveIndustry').off('click').on('click', function () {
    var name = $('#Name').val().trim();
    if (name === '') {
        toastr.error('Please enter industry!');
    } else {
        $.ajax({
            url: '/Industry/Create',
            method: 'POST',
            data: { name },
            dataType: 'json',
            success: function (result) {
                if (result.message) {
                    toastr.success(result.message);
                    btnModalClose();
                    location.reload();
                } else {
                    toastr.error('Failed to create industry');
                }
            },
            error: function (xhr, status, error) {
                toastr.error('An error occurred: ' + xhr.responseText);
            }
        });
    }
});

function btnIndustryClose() {
    $('#industryModal').modal('hide');
}

$('#industryModal').on('hidden.bs.modal', function () {     //modal reset
    $('#Name').val('');
});

// For Language
$('#addLanguage').click(function () {
    $('#languageModal').modal('show');
});

$('#saveLanguage').off('click').on('click', function () {
    var name = $('#Name').val().trim();
    if (name === '') {
        toastr.error('Please enter language!');
    } else {
        $.ajax({
            url: '/Language/Create',
            method: 'POST',
            data: { name },
            dataType: 'json',
            success: function (result) {
                if (result.message) {
                    toastr.success(result.message);
                    btnModalClose();
                    location.reload();
                } else {
                    toastr.error('Failed to create language');
                }
            },
            error: function (xhr, status, error) {
                toastr.error('An error occurred: ' + xhr.responseText);
            }
        });
    }
});

function btnLanguageClose() {
    $('#languageModal').modal('hide');
}

$('#languageModal').on('hidden.bs.modal', function () {     //modal reset
    $('#Name').val('');
});


//For Genre

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

$('#genreModal').on('hidden.bs.modal', function () {     //modal reset
    $('#Name').val('');
});



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

