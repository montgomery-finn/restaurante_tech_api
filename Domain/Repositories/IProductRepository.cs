using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IProductRepository
    {
        Task<Product> GetByID(Guid id);
        Task Add(Product product);
        Task Update(Product product);
        Task Delete(Product product);
        Task<List<Product>> GetAll();
    }
}
