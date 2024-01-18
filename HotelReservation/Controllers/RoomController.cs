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

    // Action to display all rooms for regular users
    [HttpGet]
    public IActionResult AllRooms()
    {
        var rooms = dbContext.Rooms.ToList();
        return View(rooms);
    }
}
