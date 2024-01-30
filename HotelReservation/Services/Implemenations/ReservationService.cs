using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HotelReservation.Models;
using HotelReservation.Repository.Interfaces;
using HotelReservation.Services.Interfaces;

namespace HotelReservation.Services.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly IReservationRepository _reservationRepository;

        public ReservationService(IReservationRepository reservationRepository)
        {
            _reservationRepository = reservationRepository;
        }

        public async Task<IEnumerable<DateTime>> GetReservedDatesAsync(int roomId)
        {
            var reservations = await _reservationRepository.GetReservedDatesAsync(roomId);

            // Extract StartDate and EndDate from reservations and return the list of reserved dates
            var reservedDates = reservations
                .SelectMany(r => GetDatesBetween(r.StartDate, r.EndDate))
                .ToList();

            return reservedDates;
        }

        // Other methods as needed

        private IEnumerable<DateTime> GetDatesBetween(DateTime startDate, DateTime endDate)
        {
            return Enumerable.Range(0, (endDate - startDate).Days + 1)
                .Select(offset => startDate.AddDays(offset));
        }
    }
}