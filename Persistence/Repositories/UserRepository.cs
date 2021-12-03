﻿using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly TechContext _context;

        public UserRepository()
        {
            _context = new TechContext();
        }

        public async Task Add(UserModel userModel)
        {
            var entity = userModel.ToEntity();
            await _context.Users.AddAsync(entity);
            await _context.SaveChangesAsync();
            _context.Entry(entity).State = EntityState.Unchanged;
        }

        public async Task Delete(UserModel userModel)
        {
            _context.Users.Remove(userModel.ToEntity());
            await _context.SaveChangesAsync();
        }

        public async Task<UserModel> GetByEmail(string email)
        {
            var userEntity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.Email == email);
            return userEntity?.ToModel();
        }

        public async Task<UserModel> GetByID(Guid id)
        {
            var userEntity = await _context.Users.AsNoTracking().FirstOrDefaultAsync(u => u.ID == id);
            return userEntity?.ToModel();
        }

        public async Task Update(UserModel userModel)
        {
            var userEntity = userModel.ToEntity();
            _context.Update(userEntity);
            await _context.SaveChangesAsync();
        }
    }
}
