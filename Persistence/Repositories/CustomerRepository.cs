using Domain.Models;
using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System;
using System.Threading.Tasks;

namespace Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TechContext _context;

        public CustomerRepository()
        {
            _context = new TechContext();
        }

        public async Task Add(CustomerModel customerModel)
        {
            var entity = customerModel.ToEntity();
            await _context.AddAsync(entity);
            await _context.SaveChangesAsync();
        }
    }
}
