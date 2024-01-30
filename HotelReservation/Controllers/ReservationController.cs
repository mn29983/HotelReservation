using Microsoft.AspNetCore.Mvc;
using HotelReservation.Services.Interfaces;
using System.Threading.Tasks;

public class ReservationController : Controller
{
    private readonly IReservationService _reservationService;

    public ReservationController(IReservationService reservationService)
    {
        _reservationService = reservationService;
    }

    [HttpGet("GetReservedDates")]
    public async Task<IActionResult> GetReservedDates(int roomId)
    {
        try
        {
            var reservedDates = await _reservationService.GetReservedDatesAsync(roomId);
            return Json(reservedDates);
        }
        catch (Exception ex)
        {
            // Log the exception or handle it as needed
            return StatusCode(500, "Internal Server Error");
        }
    }
}
