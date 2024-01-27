using HotelReservation.Models;

namespace HotelReservation.Models.ViewModels
{
    public class AddRoomRequest
    {
        public string RoomNumber { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public int Capacity { get; set; }

        public decimal Price { get; set; }

        public string PictureUrl1 { get; set; }
        public string PictureUrl2 { get; set; }
        public string PictureUrl3 { get; set; }

        public bool Available { get; set; }

        public List<Amenities> Amenities { get; set; }
    }
}
