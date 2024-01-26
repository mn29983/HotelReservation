using HotelReservation.Models;
using HotelReservation.Repository.Interfaces;
using HotelReservation.Services.Interfaces;
using Microsoft.EntityFrameworkCore;


namespace HotelReservation.Services.Implementations
{
    public class RoomService : IRoomService
    {
        private readonly IRepository<Room> _roomRepository;

        public RoomService(IRepository<Room> roomRepository)
        {
            _roomRepository = roomRepository ?? throw new ArgumentNullException(nameof(roomRepository));
        }

        public async Task<IEnumerable<Room>> GetAllRooms()
        {
            return await _roomRepository.GetAllAsync();
        }

        public async Task<Room> GetRoomById(int id)
        {
            return await _roomRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Room>> SearchRoomByName(string searchString)
        {
            return await _roomRepository.FindAsync(room => room.RoomNumber.Contains(searchString));
        }

        public async Task CreateRoom(Room room)
        {
            room.AvailableFrom = room.AvailableFrom?.ToUniversalTime();
            room.AvailableTo = room.AvailableTo?.ToUniversalTime();

            await _roomRepository.AddAsync(room);
        }

        public async Task UpdateRoom(Room room)
        {
            room.AvailableFrom = room.AvailableFrom?.ToUniversalTime();
            room.AvailableTo = room.AvailableTo?.ToUniversalTime();

            await _roomRepository.UpdateAsync(room);
        }

        public async Task DeleteRoom(int id)
        {
            var room = await _roomRepository.GetByIdAsync(id);
            if (room != null)
            {
                await _roomRepository.RemoveAsync(room);
            }
        }
    }
}

