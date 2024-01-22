using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelReservation.Models;
using System.Linq;

public class RoomController : Controller
{
    private readonly ApplicationDbContext dbContext;

    public RoomController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }


    [HttpGet]
    public IActionResult AllRooms(DateTime? checkIn, DateTime? checkOut)
    {
        var rooms = dbContext.Rooms.Include(r => r.Reservations).ToList();

        // Check availability for each room based on the specified dates
        if (checkIn.HasValue && checkOut.HasValue)
        {
            rooms = rooms.Where(room => IsRoomAvailable(room, checkIn.Value, checkOut.Value)).ToList();
        }

        return View("AllRooms", rooms);
    }

    public bool IsRoomAvailable(Room room, DateTime checkIn, DateTime checkOut)
    {
        // Check if there are any reservations that overlap with the specified dates
        return !room.Reservations.Any(r => (checkIn < r.CheckOut && checkOut > r.CheckIn));
    }

    // Action to display all rooms for regular users with availability check
    [HttpGet]
    public IActionResult AllRoomsAvailability(DateTime? checkIn, DateTime? checkOut)
    {
        // Retrieve all rooms from the database
        var rooms = dbContext.Rooms.ToList();

        // Check availability for each room based on the specified dates
        if (checkIn.HasValue && checkOut.HasValue)
        {
            rooms = rooms.Where(room => IsRoomAvailable(room, checkIn.Value, checkOut.Value)).ToList();
        }

        // Pass the list of rooms to the view
        return View("AllRooms", rooms);
    }
}

