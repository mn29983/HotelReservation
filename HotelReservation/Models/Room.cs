using System;
using System.ComponentModel.DataAnnotations;


namespace HotelReservation.Models
{

    public enum Amenities
    {
        Toiletries,
        WiFi,
        TV,
        AirConditioning,
        MiniBar,
        RoomService,
        CoffeeMaker,
        HairDryer,
        NonSmoking,
        FreeParking,
    }
    public class Room
    {
        public int RoomId { get; set; }

        [Required]
        [Display(Name = "Room Number")]
        public string RoomNumber { get; set; }

        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Capacity")]
        public int Capacity { get; set; }

        [Required]
        [Display(Name = "Price")]
        public decimal Price { get; set; }

        [Display(Name = "Picture URL")]
        public string PictureUrl1 { get; set; }
        public string PictureUrl2 { get; set; }
        public string PictureUrl3 { get; set; }

        [Display(Name = "Available")]
        public bool Available { get; set; }

        [Display(Name = "Available From")]
        [DataType(DataType.Date)]
        public DateTime? AvailableFrom { get; set; }

        [Display(Name = "Available To")]
        [DataType(DataType.Date)]
        public DateTime? AvailableTo { get; set; }

        public List<Amenities> Amenities { get; set; }
    }
}