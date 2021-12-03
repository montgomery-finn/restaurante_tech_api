using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Threading.Tasks;
using Domain.Models;

namespace Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TechContext _context;

        public UserRepository()
        {
            _context = new TechContext();
        }

        public async Task Add(User user)
        {
            await _context.Users.AddAsync(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(User user)
        {
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public Task<User> GetByEmail(string email)
        {
            return _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
        }

        public Task<User> GetByID(Guid id)
        {
            return _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.ID == id);
        }

        public async Task Update(User user)
        {
            _context.Update(user);
            await _context.SaveChangesAsync();
        }
    }
}
