using HotelReservation.Models;
using HotelReservation.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

public class AdminController : Controller
{
    private readonly UserManager<ApplicationUser> _userManager;
    private readonly ApplicationDbContext _dbContext;
    private readonly IRoomService _roomService;

    public AdminController(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, IRoomService roomService)
    {
        _userManager = userManager;
        _dbContext = dbContext;
        _roomService = roomService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> Index()
    {
        var viewModel = new AdminDashboardViewModel
        {
            WelcomeMessage = "Welcome to the Admin Dashboard!",
            Users = await _userManager.Users.ToListAsync(),
            Rooms = await _dbContext.Rooms.ToListAsync(),
            NewRoom = new Room()
        };

        return View(viewModel);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public IActionResult ManageUsers()
    {
        var viewModel = new AdminDashboardViewModel
        {
            WelcomeMessage = "Manage Users",
            Users = _userManager.Users.ToList()
        };

        return View("Index", viewModel);
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> ManageRooms()
    {
        var viewModel = new AdminDashboardViewModel
        {
            WelcomeMessage = "Manage Rooms",
            Rooms = await _dbContext.Rooms.ToListAsync(),
        };

        return View("Index", viewModel);
    }

    [HttpGet("all")]
    public async Task<IActionResult> AllRoom()
    {
        var room = await _roomService.GetAllRooms();
        if (Request.Headers["Accept"].ToString().Contains("text/html"))
        {
            return View(room);
        }
        else
        {
            return Ok(room);
        }
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetRoomById(int id)
    {
        var room = await _roomService.GetRoomById(id);
        if (room == null)
        {
            return NotFound();
        }

        if (Request.Headers["Accept"].ToString().Contains("text/html"))
        {
            return View("SingleRoom", room);
        }
        else
        {
            return Ok(room);
        }
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> CreateRoom(Room newRoom)
    {

        if (ModelState.IsValid)
        {
            _dbContext.Rooms.Add(newRoom);
            await _dbContext.SaveChangesAsync();

            // Redirect to the ManageRooms action to show the updated list
            return RedirectToAction("ManageRooms");
        }

        // If the model is not valid, return to the Index page with the validation errors
        var viewModel = new AdminDashboardViewModel
        {
            WelcomeMessage = "Welcome to the Admin Dashboard!",
            Users = await _userManager.Users.ToListAsync(),
            Rooms = await _dbContext.Rooms.ToListAsync(),
            NewRoom = newRoom
        };

        return View("Index", viewModel);
    }
}
