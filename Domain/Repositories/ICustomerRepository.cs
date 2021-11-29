using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface ICustomerRepository
    {
        Task Add(CustomerModel customerModel);
        Task<CustomerModel> GetByCPF(string cpf);
    }
}
