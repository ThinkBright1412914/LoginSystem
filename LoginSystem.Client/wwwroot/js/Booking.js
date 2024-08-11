
$('#onShowChange').on('change', function () {
    var showId = parseInt($(this).val());
    $.ajax({
        url: '/Booking/GetOptionByShowId',
        data: { 'Id': showId },
        dataType: 'Json',
        type: 'Get',
        success: function (result) {
            $('#ticketPrice').val(result.ticketPrice);
        }
    })
});



const container = document.querySelector(".movie-seat-container");
const seats = document.querySelectorAll(".seat-row .seat:not(.occupied)");
const count = document.getElementById("count");
const total = document.getElementById("totalAmount");
const movieSelect = document.getElementById("movie");

populateUI();



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

    const seat = [...selectedSeats].map(seat => seat.innerText);

    document.getElementById("seat-details").value = seat.join(", ");
    document.getElementById("totalAmount").value = selectedSeatsCount * ticketPrice;

    setMovieData(movieSelect.selectedIndex, movieSelect.value);
}

// Get data from localstorage and populate UI
function populateUI() {
    const selectedSeats = JSON.parse(localStorage.getItem("selectedSeats"));

    if (selectedSeats !== null && selectedSeats.length > 0) {
        seats.forEach((seat, index) => {
            if (selectedSeats.indexOf(index) > -1) {
                seat.classList.add("selected");
            }
        });
    }

    const selectedMovieIndex = localStorage.getItem("selectedMovieIndex");

    if (selectedMovieIndex !== null) {
        movieSelect.selectedIndex = selectedMovieIndex;
    }
}

// Movie select event
movieSelect.addEventListener("change", (e) => {
    ticketPrice = +e.target.value;
    setMovieData(e.target.selectedIndex, e.target.value);
    updateSelectedCount();
});


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
