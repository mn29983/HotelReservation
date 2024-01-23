using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelReservation.Models;
using HotelReservation.Services.Interfaces;
using HotelReservation.Services;
using System.Linq;

public class RoomController : Controller
{
    private readonly IRoomService _roomService;

    public RoomController(IRoomService roomService)
    {
        _roomService = roomService ?? throw new ArgumentNullException(nameof(roomService));
    }

    [HttpGet("all")]
    public async Task<IActionResult> AllRooms()
    {
        var rooms = await _roomService.GetAllRooms();
        return View(rooms);
    }

    //public bool IsRoomAvailable(Room room, DateTime checkIn, DateTime checkOut)
    //{
    //    // Check if there are any reservations that overlap with the specified dates
    //    return !room.Reservations.Any(r => (checkIn < r.CheckOut && checkOut > r.CheckIn));
    //}

    // Action to display all rooms for regular users with availability check
    //[HttpGet]
    //public IActionResult AllRoomsAvailability(DateTime? checkIn, DateTime? checkOut)
    //{
    //    // Retrieve all rooms from the database
    //    var rooms = dbContext.Rooms.ToList();

    //    // Check availability for each room based on the specified dates
    //    if (checkIn.HasValue && checkOut.HasValue)
    //    {
    //        rooms = rooms.Where(room => IsRoomAvailable(room, checkIn.Value, checkOut.Value)).ToList();
    //    }

    //    // Pass the list of rooms to the view
    //    return View("AllRooms", rooms);
    //}
}

