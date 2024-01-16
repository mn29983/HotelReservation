using System;
using Microsoft.EntityFrameworkCore;
using HotelReservation.Repositories.Interfaces;

namespace HotelReservation.Repositories.Implementations
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _context;

        public Repository(ApplicationDbContext context)
        {
            _context = context;
        }
    
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            return await _context.Set<T>().ToListAsync();
        }

        async Task IRepository<T>.Add(T entity)
        {
            _context.Set<T>().Add(entity);
            await _context.SaveChangesAsync();
        }

        async Task IRepository<T>.Delete(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            if (entity != null)
            {
                _context.Set<T>().Remove(entity);
                await _context.SaveChangesAsync();
            }
        }

        async Task<T> IRepository<T>.GetById(int id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        async Task IRepository<T>.Update(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        // Implement other CRUD methods
    }

}

