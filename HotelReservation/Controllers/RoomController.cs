using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelReservation.Models;
using HotelReservation.Models.Reservations;
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

    [HttpGet("booking/{roomId}")]
    public IActionResult Booking(int roomId)
    {
        ViewBag.RoomId = roomId;
        return View();
    }


    [HttpPost]
    public IActionResult ReserveRoom(int roomId, string from, string to, string name, string lastName, string email, string phone, string cardName, string cardNumber, string expiryDate, string cvv, string specialRequests, bool agreeTerms)
    {
        if (string.IsNullOrEmpty(cardName))
        {
            return Json(new { success = false, message = "CardName cannot be null or empty." });
        }

        if (!DateTime.TryParse(from, out DateTime fromDate) || !DateTime.TryParse(to, out DateTime toDate) || !DateTime.TryParse(expiryDate, out DateTime expiryDateValue))
        {
            return Json(new { success = false, message = "Invalid date format." });
        }

        var reservation = new Reservation
        {
            RoomId = roomId,
            StartDate = fromDate.ToUniversalTime(),
            EndDate = toDate.ToUniversalTime(),
            SpecialRequests = specialRequests
        };

        var guest = new Guest
        {
            Name = name,
            LastName = lastName,
            Email = email,
            Phone = phone
        };

        var billingInfo = new BillingInfo
        {
            CardName = cardName,
            CardNumber = cardNumber,
            ExpiryDate = expiryDateValue.ToUniversalTime(),
            CVV = cvv
        };

        reservation.Guest = guest;
        reservation.BillingInfo = billingInfo;


            _dbContext.Reservations.Add(reservation);
            _dbContext.SaveChanges();
            return Json(new { success = true });

    }

}
