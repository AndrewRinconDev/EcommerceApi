using EcommerceApi.Models.Database;
using EcommerceApi.Models.Dto;

namespace EcommerceApi.Services.Contracts
{
    public interface IUserService : IBaseService<User>
    {
        public Task<IEnumerable<User>> GetByRole(Guid roleId);

        public Task<UserLoggedDto?> Login(UserLoginDto userLoginDto);

        public Task<User> SaveUser(UserDto user);

        public Task<User> UpdateUser(UserDto user);

        public Task<User?> DeleteUser(Guid id);

        public Task<User> ChangePassword(UserChangePasswordDto userChangePasswordDto);

        public Task<User?> GetByEmail(string email);

        public Task<User?> GetIncludeRole(Guid id);
    }
}
