using Domain.Repositories;
using Microsoft.EntityFrameworkCore;
using Persistence.Data;
using System.Linq;
using System.Threading.Tasks;
using Domain.Models;

namespace Persistence.Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly TechContext _context;

        public CustomerRepository()
        {
            _context = new TechContext();
        }

        public async Task Add(Customer customer)
        {
            await _context.AddAsync(customer);
            await _context.SaveChangesAsync();
        }

        public async Task<Customer> GetByCPF(string cpf)
        {
            return await _context.Customers.Where(c => c.CPF == cpf).FirstOrDefaultAsync();
        }
    }
}
