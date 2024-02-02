using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HotelReservation.Models.Reservations;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Controllers
{

    [Authorize(Roles = "Admin")]
    public class DashboardController : Controller
    {


        private readonly ApplicationDbContext _dbContext;

        public DashboardController(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }


        public IActionResult Index()
        {
            return View();
        }

        public IActionResult AllReservations()
        {
            var reservations = _dbContext.Reservations
                .Include(r => r.Guest)
                .Include(r => r.BillingInfo)
                .ToList();

            return View(reservations);
        }
    }


}
