using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
using HotelReservation.Models;

namespace HotelReservation.Services.Interfaces
{
    public interface IReservationService
    {
        Task<IEnumerable<DateTime>> GetReservedDatesAsync(int roomId);
    }
}