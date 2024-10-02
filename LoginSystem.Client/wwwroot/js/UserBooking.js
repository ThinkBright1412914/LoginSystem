var dataTable;
$(document).ready(function () {  
    LoadData(false);
    LoadHistoryData(true);
})

function LoadData(status) {
    if ($.fn.DataTable.isDataTable('#tblTickets')) {
        $('#tblTickets').DataTable().ajax.reload(null, false);
    }
    else
    {
        var dataTable =
            $('#tblTickets').DataTable({
                "processing": true,
                "searching": false,
                "ajax": {
                    "url": "/Booking/UserTicketsInfo/",
                    "type": "GET",
                    "data": { isHistory: status },
                },
                "columns": [
                    { "data": "show", "width": "5%" },
                    { "data": "movie", "width": "15%" },
                    { "data": "date", "width": "15%" },
                    { "data": "time", "width": "15%" },
                    { "data": "seatDetails", "width": "15%" },
                    //{
                    //    "data": "bookingId",
                    //    "render": function (data, type, row) {
                    //        return '<button class="btn btn-download" data-booking-id="' + data + '" title="Download">' +
                    //            '<i class="fa fa-download"></i>' +
                    //            '</button>';
                    //    },
                    //    "width": "10%"
                    //}
                ]
            });
    }
   
}


function LoadHistoryData(status) {
    if ($.fn.DataTable.isDataTable('#tblHistoryTickets')) {
        $('#tblHistoryTickets').DataTable().ajax.reload(null, false);
    }
    else
    {
        var dataTable =
            $('#tblHistoryTickets').DataTable({
                "processing": true,
                "searching": false,
                "ajax": {
                    "url": "/Booking/UserTicketsInfo/",
                    "type": "GET",
                    "data": { isHistory: status },
                },
                "columns": [
                    { "data": "show", "width": "5%" },
                    { "data": "movie", "width": "15%" },
                    { "data": "date", "width": "15%" },
                    { "data": "time", "width": "15%" },
                    { "data": "seatDetails", "width": "15%" },
                    //{
                    //    "data": "bookingId",
                    //    "render": function (data, type, row) {
                    //        return '<button class="btn btn-download" data-booking-id="' + data + '" title="Download">' +
                    //            '<i class="fa fa-download"></i>' +
                    //            '</button>';                        },
                    //    "width": "10%"
                    //}
                ]
            });
    }
}

$('#nav-history-tab').click(function () {
    LoadHistoryData(true);
})

$('#nav-booked-tab').click(function () {
    LoadData(false);
})
