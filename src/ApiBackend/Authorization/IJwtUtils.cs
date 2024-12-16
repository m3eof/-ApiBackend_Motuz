using ApiBackend.Entities;
using ApiBackend.Models;

namespace ApiBackend.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(User account);
        public int? ValidateJwtToken(string token);
        public Task<RefreshToken> GenerateRefreshToken(string ipAddress);
    }
}
