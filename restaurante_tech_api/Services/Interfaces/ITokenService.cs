using Domain.Models;

namespace restaurante_tech_api.Services.Interfaces
{
    public interface ITokenService
    {
        public string GenerateToken(User user);
    }
}
