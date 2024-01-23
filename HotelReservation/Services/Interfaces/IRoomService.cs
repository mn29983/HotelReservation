using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using HotelReservation.Models;

namespace HotelReservation.Services.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<Room>> GetAllRooms();
        Task<Room> GetRoomById(int id);
        Task<IEnumerable<Room>> SearchRoomByName(string searchString);
        Task CreateRoom(Room room);
        Task UpdateRoom(Room room);
        Task DeleteRoom(int id);
    }
}