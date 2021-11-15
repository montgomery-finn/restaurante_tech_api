using Domain.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IProductRepository
    {
        Task<ProductModel> GetByID(Guid id);
        Task Add(ProductModel productModel);
        Task Update(ProductModel productModel);
        Task Delete(ProductModel productModel);
        Task<List<ProductModel>> GetAll();
    }
}
