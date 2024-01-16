using System;
using HotelReservation.Models;

namespace HotelReservation.Services.Interfaces
{
	public interface IRoomService
	{
		Task<IEnumerable<Room>> GetAllRooms();

		Task<Room> GetRoomById(int id);
	}
}

