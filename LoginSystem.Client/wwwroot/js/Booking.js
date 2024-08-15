
$('#onShowChange').on('change', function () {
    var showId = parseInt($(this).val());
    $.ajax({
        url: '/Booking/GetOptionByShowId',
        data: { 'Id': showId },
        dataType: 'Json',
        type: 'Get',
        success: function (result) {
            $('#ticketPrice').val(result.ticketPrice);
            populateUI(result.reserveSeats);
        }
    })
});


const container = document.querySelector(".movie-seat-container");
const seats = document.querySelectorAll(".seat-row .seat:not(.occupied)");
const count = document.getElementById("count");
const total = document.getElementById("totalAmount");
const movieSelect = document.getElementById("movie");


// Save selected movie index and price
function setMovieData(movieIndex, moviePrice) {
    localStorage.setItem("selectedMovieIndex", movieIndex);
    localStorage.setItem("selectedMoviePrice", moviePrice);
}

// Update total and count
function updateSelectedCount() {
    let ticketPrice = parseInt($('#ticketPrice').val());
    const selectedSeats = document.querySelectorAll(".seat-row .seat.selected");

    const seatsIndex = [...selectedSeats].map((seat) => [...seats].indexOf(seat));

    localStorage.setItem("selectedSeats", JSON.stringify(seatsIndex));

    const selectedSeatsCount = selectedSeats.length;

    count.innerText = selectedSeatsCount;

    document.getElementById("ticketCount").value = selectedSeatsCount;
    document.getElementById("reservedSeats").value = JSON.stringify(seatsIndex);

    const seat = [...selectedSeats].map(seat => seat.innerText);

    document.getElementById("seat-details").value = seat.join(", ");
    document.getElementById("totalAmount").value = selectedSeatsCount * ticketPrice;

}

// Get data from localstorage and populate UI
function populateUI(value) {
    seats.forEach((seat) => {
        seat.classList.remove("occupied");
    });

    if (value !== null) {
        const selectedSeats = JSON.parse(value);

        if (selectedSeats !== null && selectedSeats.length > 0) {
            seats.forEach((seat, index) => {
                if (selectedSeats.indexOf(index) > -1) {
                    seat.classList.add("occupied");
                }
            });
        }

        const selectedMovieIndex = value;

        if (selectedMovieIndex !== null) {
            movieSelect.selectedIndex = selectedMovieIndex;
        }
    }
}

// Seat click event
container.addEventListener("click", (e) => {
    if (
        e.target.classList.contains("seat") &&
        !e.target.classList.contains("occupied")
    ) {
        e.target.classList.toggle("selected");

        updateSelectedCount();
    }
});

// Initial count and total set
updateSelectedCount();
