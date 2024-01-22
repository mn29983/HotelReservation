using HotelReservation.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using HotelReservation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace HotelReservation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminRoomController : Controller
    {
        private readonly ApplicationDbContext dbContext;

        public AdminRoomController(ApplicationDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var rooms = dbContext.Rooms.ToList(); // Retrieve the list of rooms from your database
            return View(rooms);
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddRoomRequest addRoomRequest)
        {
            if (ModelState.IsValid)
            {
                var room = new Room
                {
                    RoomNumber = addRoomRequest.RoomNumber,
                    Type = addRoomRequest.Type,
                    Description = addRoomRequest.Description,
                    Capacity = addRoomRequest.Capacity,
                    Price = addRoomRequest.Price,
                    PictureUrl = addRoomRequest.PictureUrl,
                    Available = addRoomRequest.Available,
                    AvailableFrom = addRoomRequest.AvailableFrom,
                    AvailableTo = addRoomRequest.AvailableTo
                };

                dbContext.Rooms.Add(room);
                dbContext.SaveChanges();

                return RedirectToAction("Add");
            }

            return View(addRoomRequest);
        }

        [HttpGet]
        public IActionResult Edit(int id)
        {
            var room = dbContext.Rooms.Find(id);

            if (room == null)
            {
                return NotFound();
            }

            var editRoomRequest = new EditRoomRequest
            {
                RoomNumber = room.RoomNumber,
                Type = room.Type,
                Description = room.Description,
                Capacity = room.Capacity,
                Price = room.Price,
                PictureUrl = room.PictureUrl,
                Available = room.Available,
                AvailableFrom = room.AvailableFrom,
                AvailableTo = room.AvailableTo
            };

            return View(editRoomRequest);
        }

        [HttpPost]
        public IActionResult Edit(EditRoomRequest editRoomRequest)
        {
            if (ModelState.IsValid)
            {
                var room = dbContext.Rooms.Find(editRoomRequest.RoomId);

                if (room == null)
                {
                    return NotFound();
                }

                room.RoomNumber = editRoomRequest.RoomNumber;
                room.Type = editRoomRequest.Type;
                room.Description = editRoomRequest.Description;
                room.Capacity = editRoomRequest.Capacity;
                room.Price = editRoomRequest.Price;
                room.PictureUrl = editRoomRequest.PictureUrl;
                room.Available = editRoomRequest.Available;

                dbContext.Entry(room).State = EntityState.Modified;
                dbContext.SaveChanges();

                return RedirectToAction("Index");
            }

            return View(editRoomRequest);
        }
    }
}
