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
            return await _roomRepository.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Room>> SearchRoomByName(string searchString)
        {
            // Implement the search logic in the repository or service
            // For now, returning an empty list
            return new List<Room>();
        }

        public async Task CreateRoom(Room room)
        {
            await _roomRepository.AddAsync(room);
        }
        public async Task UpdateRoom(Room room)
        {
            await _roomRepository.UpdateAsync(room);
        }

        public async Task DeleteRoom(int id)
        {
            await _roomRepository.DeleteAsync(id);
        }

    }
}

