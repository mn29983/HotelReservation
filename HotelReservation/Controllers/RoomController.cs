using System;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HotelReservation.Services.Interfaces;

namespace HotelReservation.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("rooms")]
    public class RoomController : Controller
    {
        private readonly IRoomService _roomService;

        public RoomController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet]
        public async Task<IActionResult> AllRooms()
        {
            var rooms = await _roomService.GetAllRooms();
            if (Request.Headers["Accept"].ToString().Contains("text/html"))
            {
                return View(rooms);
            }
            else
            {
                return Ok(rooms);
            }
        }

        [HttpGet("rooms/{id}")]
        public async Task<IActionResult> GetRoom(int id)
        {
            var result = await _roomService.GetRoomById(id);
            return Ok(result);
        }
    }

}

