namespace HotelReservation.Models.ViewModels
{
    public class AddRoomRequest
    {
        public string RoomNumber { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public int Capacity { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl { get; set; }

        public bool Available { get; set; }

        public DateTime AvailableFrom { get; set; }
        public DateTime AvailableTo { get; set; }


    }
}
