using System;
using HotelReservation.Models;
using HotelReservation.Repositories.Interfaces;
using HotelReservation.Services.Interfaces;

namespace HotelReservation.Services.Implementations
{
	public class RoomService : IRoomService
	{
        private readonly IRepository<Room> _roomRepository;

        public RoomService(IRepository<Room> RoomRepository)
        {
            _roomRepository = RoomRepository;
        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            return await _roomRepository.GetAllAsync();
        }

        public async Task<Room> GetRoomById(int id)
        {
            return await _roomRepository.GetById(id);
        }
    }
}

