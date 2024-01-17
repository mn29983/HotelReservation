﻿namespace HotelReservation.Models
{
    public class Room
    {
        public Guid Id { get; set; }
        public string RoomNumber { get; set; }
        public string Type { get; set; }
        public string Description { get; set; }

        public int Capacity { get; set; }

        public decimal Price { get; set; }

        public List<string> Pictures { get; set; }

        public bool Available { get; set; }

    }
}
