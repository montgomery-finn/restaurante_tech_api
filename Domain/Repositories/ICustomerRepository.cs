using Domain.Models;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task Add(Customer customer);
        Task<Customer> GetByCPF(string cpf);
    }
}
