﻿namespace HotelReservation.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string RoomNumber { get; set; }

        public string Type { get; set; }

        public int Capacity { get; set; }

        public decimal Price { get; set; }

        public bool Available { get; set; }

    }
}
