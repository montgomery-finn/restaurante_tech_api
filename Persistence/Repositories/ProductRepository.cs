using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Domain.Models;

namespace Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly TechContext _context;

        public ProductRepository()
        {
            _context = new TechContext();
        }

        public async Task Add(Product product)
        {
            await _context.AddAsync(product);
            await _context.SaveChangesAsync();
        }

        public Task<List<Product>> GetAll()
        {
            return  _context.Products.ToListAsync();
        }

        public async Task Delete(Product product)
        {
            _context.Remove(product);
            await _context.SaveChangesAsync();
        }

        public Task<Product> GetByID(Guid id)
        {
            return _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ID == id);
        }

        public async Task Update(Product product)
        {
            _context.Update(product);
            await _context.SaveChangesAsync();
        }

    }
}
