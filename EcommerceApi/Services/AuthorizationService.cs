using EcommerceApi.Models.Database;
using EcommerceApi.Repositories.Contracts;
using EcommerceApi.Services.Contracts;

namespace EcommerceApi.Services
{
    public class AuthorizationService : IAuthorizationService
    {
        public AuthorizationService() { }
        
        public async Task<string> GenerateToken(User user)
        {
            // TODO Generar token 
            return "";
        }
    }
}
