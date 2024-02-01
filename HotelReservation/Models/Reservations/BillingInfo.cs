namespace HotelReservation.Models.Reservations
{
    public class BillingInfo
    {
        public int Id { get; set; }
        public string CardName { get; set; }
        public string CardNumber { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string CVV { get; set; }
    }
}