using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly TechContext _context;

        public ProductRepository()
        {
            _context = new TechContext();
        }

        public async Task Add(ProductModel productModel)
        {
            var entity = productModel.ToEntity();
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<List<ProductModel>> GetAll()
        {
            var productEntities = await _context.Products.AsNoTracking().ToListAsync();
            return productEntities.Select(p => p.ToModel()).ToList();
        }

        public async Task Delete(ProductModel productModel)
        {
            var entity = productModel.ToEntity();
            _context.Attach(entity);
            _context.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<ProductModel> GetByID(Guid id)
        {
            var entity = await _context.Products.AsNoTracking().FirstOrDefaultAsync(p => p.ID == id);
            return entity?.ToModel();
        }

        public async Task Update(ProductModel productModel)
        {
            var entity = productModel.ToEntity();
            _context.Attach(entity);
            _context.Update(entity);
            await _context.SaveChangesAsync();
        }

    }
}
