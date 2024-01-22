using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using HotelReservation.Models;
using System;

public class ReservationController : Controller
{
    private readonly ApplicationDbContext dbContext;

    public ReservationController(ApplicationDbContext dbContext)
    {
        this.dbContext = dbContext;
    }

    [HttpPost]
    public IActionResult MakeReservation(int roomId, DateTime checkInDate, DateTime checkOutDate)
    {
        var room = dbContext.Rooms
            .Include(r => r.Reservations)
            .FirstOrDefault(r => r.Id == roomId);

        if (room != null && IsRoomAvailable(room, checkInDate, checkOutDate))
        {
            var reservation = new Reservation
            {
                CheckIn = checkInDate,
                CheckOut = checkOutDate
            };

            room.Reservations.Add(reservation);
            dbContext.SaveChanges();

            // Update the room's availability
            room.AvailableFrom = checkOutDate.AddDays(1);

            dbContext.SaveChanges();

            // Handle successful reservation, e.g., show confirmation to the user
            return RedirectToAction("ReservationConfirmation");
        }

        // Handle unsuccessful reservation, e.g., show error message to the user
        return RedirectToAction("ReservationError");
    }

    private bool IsRoomAvailable(Room room, DateTime checkIn, DateTime checkOut)
    {
        return room.AvailableFrom <= checkIn && room.AvailableTo >= checkOut &&
               !room.Reservations.Any(r => checkIn < r.CheckOut && checkOut > r.CheckIn);
    }
}

