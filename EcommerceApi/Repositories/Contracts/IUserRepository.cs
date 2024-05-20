using EcommerceApi.Models.Database;
using EcommerceApi.Models.Dto;

namespace EcommerceApi.Repositories.Contracts
{
    public interface IUserRepository : IBaseRepository<User>
    {
        public Task<IEnumerable<User>> GetByRole(Guid roleId);

        public Task<User?> GetIncludeRole(Guid id);

        public Task<User?> GetByEmail(string email);

        public Task<User?> Login(UserLoginDto userLoginDto);
    }
}
