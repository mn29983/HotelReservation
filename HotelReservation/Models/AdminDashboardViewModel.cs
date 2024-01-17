namespace HotelReservation.Models
{
    public class AdminDashboardViewModel
    {
        public string WelcomeMessage { get; set; }
        public List<ApplicationUser> Users { get; set; }
        public List<Room> Rooms { get; set; }

        public Room NewRoom { get; set; }
    }
}

