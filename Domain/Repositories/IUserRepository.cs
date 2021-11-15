using Domain.Models;
using System;
using System.Threading.Tasks;

namespace Domain.Repositories
{
    public interface IUserRepository
    {
        Task<UserModel> GetByID(Guid id);
        Task<UserModel> GetByEmail(string email);
        Task Add(UserModel ProductModel);
        Task Update(UserModel ProductModel);
        Task Delete(UserModel ProductModel);
    }
}
