using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
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

        public async Task Create(ProductModel productModel)
        {
            var entity = productModel.ToEntity();
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public Task Delete(ProductModel productModel)
        {
            throw new NotImplementedException();
        }

        public Task<ProductModel> GetByID(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Update(ProductModel productModel)
        {
            throw new NotImplementedException();
        }
    }
}
