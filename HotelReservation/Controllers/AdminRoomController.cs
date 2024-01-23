using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HotelReservation.Services.Interfaces;
using HotelReservation.Models;
using HotelReservation.Services;

namespace HotelReservation.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("adminroom")]
    public class AdminRoomController : Controller
    {
        private readonly IRoomService _roomService;

        public AdminRoomController(IRoomService roomService)
        {
            _roomService = roomService ?? throw new ArgumentNullException(nameof(roomService));
        }

        [HttpGet("all")]
        public async Task<IActionResult> AllRooms()
        {
            var rooms = await _roomService.GetAllRooms();
            return View(rooms);
        }

        [HttpGet("create")]
        public ActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Room room)
        {
            if (ModelState.IsValid)
            {
                await _roomService.CreateRoom(room);
                return RedirectToAction("AllRooms");
            }
            return View(room);
        }


        [Authorize(Roles = "Admin")]
        [HttpGet("update/{id}")]
        public async Task<IActionResult> Update(int id)
        {
            var room = await _roomService.GetRoomById(id);
            if (room == null)
            {
                return NotFound();
            }
            return View("Update", room);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("update/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(int id, Room room)
        {
            if (id != room.RoomId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _roomService.UpdateRoom(room);
                return RedirectToAction("AllRooms");
            }

            // If ModelState is not valid, return the same view with the model
            return View(room);
        }

        [HttpGet("delete/{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var room = await _roomService.GetRoomById(id);
            if (room == null)
            {
                return NotFound();
            }

            return View(room);
        }

        [HttpPost("delete/{id}")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _roomService.DeleteRoom(id);
                return RedirectToAction("AllRooms");
            }
            catch (Exception)
            {
                // Handle exceptions or show an error view
                return StatusCode(500);
            }
        }
    }
}
