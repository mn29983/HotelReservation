using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelReservation.Models;
using HotelReservation.Services.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

public class RoomController : Controller
{
    private readonly IRoomService _roomService;
    private readonly ApplicationDbContext _dbContext;

    public RoomController(IRoomService roomService, ApplicationDbContext dbContext)
    {
        _roomService = roomService ?? throw new ArgumentNullException(nameof(roomService));
        _dbContext = dbContext;
    }

    [HttpGet("all")]
    public async Task<IActionResult> AllRooms()
    {
        var rooms = await _roomService.GetAllRooms();
        return View(rooms);
    }

    [HttpGet("details/{id}")]
    public async Task<IActionResult> RoomDetails(int id)
    {
        var room = await _roomService.GetRoomById(id);

        if (room == null)
        {
            return NotFound();
        }

        return View(room);
    }

    [HttpPost]
    public IActionResult ReserveRoom(int roomId, DateTime from, DateTime to)
    {
        // Perform validation and add the reservation to the database
        var reservation = new Reservation
        {
            RoomId = roomId,
            StartDate = DateTime.SpecifyKind(from, DateTimeKind.Utc),
            EndDate = DateTime.SpecifyKind(to, DateTimeKind.Utc)
        };

        _dbContext.Reservations.Add(reservation);
        _dbContext.SaveChanges();

        return Json(new { success = true });
    }

}
