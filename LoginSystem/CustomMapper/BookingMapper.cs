using LoginSystem.Domain.Model;
using LoginSystem.ViewModel;

namespace LoginSystem.CustomMapper
{
	public static class BookingMapper
	{
		public static BookingDto ToModel(Booking entity)
		{
			BookingDto model = new BookingDto();
			model.Id = entity.Id;
			model.UserName = entity.User.UserName;
			model.BookingDate = entity.Date.ToString("dd/MM/yyyy");
			model.NoOfTicket = entity.No_of_Tickets;
			model.BookingStatus = "Paid";
			model.SeatDetails = entity.SeatDetails;
			return model;
		}

		public static Booking ToEntity(BookingDto model)
		{
			Booking dbBooking = new Booking();
			dbBooking.UserId = model.UserId;
			dbBooking.ShowId = model.ShowId;
			dbBooking.No_of_Tickets = model.NoOfTicket;
			dbBooking.Date = DateTime.Parse(model.BookingDate);
			dbBooking.TotalAmount = model.TotalAmount;
			dbBooking.SeatDetails = model.SeatDetails;
			return dbBooking;
		}
	}
}
