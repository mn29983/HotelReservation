using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HotelReservation.Models.Reservations;
using HotelReservation.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace HotelReservation.Repository.Implementations
{
    public class ReservationRepository : Repository<Reservation>, IReservationRepository
    {
        public ReservationRepository(ApplicationDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Reservation>> GetReservedDatesAsync(int roomId)
        {
            var reservations = await _dbSet
                .Where(r => r.RoomId == roomId)
                .ToListAsync();

            return reservations;
        }
    }
}
