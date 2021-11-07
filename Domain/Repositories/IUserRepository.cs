using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel> GetByID(Guid id);
        Task<UserModel> GetByEmail(string email);
        Task Create(UserModel userModel);
        Task Update(UserModel userModel);
        Task Delete(UserModel userModel);
    }
}
