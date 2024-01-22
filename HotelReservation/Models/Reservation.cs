namespace HotelReservation.Models
{
    public class Reservation
    {
        public int Id { get; set; }
        public string Guest { get; set; }
        public DateTime CheckIn { get; set; }
        public DateTime CheckOut { get; set; }

        public int RoomId { get; set; }

        public Room Room { get; set; }
    }
}
