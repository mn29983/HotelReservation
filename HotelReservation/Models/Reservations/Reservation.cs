namespace HotelReservation.Models.Reservations
{
    public class Reservation
    {
        public int Id { get; set; }
        public int RoomId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

        public string? SpecialRequests { get; set; }

        public Guest Guest { get; set; }
        public BillingInfo BillingInfo { get; set; }
    }
}
