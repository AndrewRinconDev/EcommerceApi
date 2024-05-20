using EcommerceApi.Models.Database;

namespace EcommerceApi.Services.Contracts
{
    public interface IAuthorizationService
    {
        public Task<string> GenerateToken(User user);
    }
}
