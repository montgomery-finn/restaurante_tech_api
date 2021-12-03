using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<User> GetByID(Guid id);
        Task<User> GetByEmail(string email);
        Task Add(User Product);
        Task Update(User Product);
        Task Delete(User Product);
    }
}
