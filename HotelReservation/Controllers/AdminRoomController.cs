﻿using HotelReservation.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using HotelReservation.Models;
using Microsoft.AspNetCore.Authorization;

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
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Add(AddRoomRequest addRoomRequest)
        {
            var room = new Room
            {
                RoomNumber = addRoomRequest.RoomNumber,
                Type = addRoomRequest.Type,
                Description = addRoomRequest.Description,
                Capacity = addRoomRequest.Capacity,
                Price = addRoomRequest.Price,
                Pictures = addRoomRequest.Pictures,
                Available = addRoomRequest.Available,
            };

            dbContext.Rooms.Add(room);
            dbContext.SaveChanges();

            return View("Add");
        }
    }
}