using EcommerceApi.Context;
using EcommerceApi.Helpers;
using EcommerceApi.Models.Database;
using EcommerceApi.Models.Dto;
using EcommerceApi.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;

namespace EcommerceApi.Repositories
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        private readonly CryptographyHelper _cryptographyHelper;

        public UserRepository(EcommerceDbContext context, IConfiguration configuration) : base(context) {
            _cryptographyHelper = new CryptographyHelper(configuration);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _context.Set<User>()
                .Where(_ => _.email == email).FirstOrDefaultAsync();
        }

        public async Task<IEnumerable<User>> GetByRole(Guid roleId)
        {
            return await _context.Set<User>()
                .Where(_ => _.roleId == roleId).ToListAsync();
        }

        public async Task<User?> GetIncludeRole(Guid id)
        {
            return await _context.Set<User>()
                .Where(_ => _.id == id)
                .Include(_ => _.role)
                .FirstOrDefaultAsync();
        }

        public async Task<User?> Login(UserLoginDto userLoginDto)
        {
            string encryptedPassword = _cryptographyHelper.Encrypt(userLoginDto.password);

            return await _context.Set<User>()
                .Where(_ => _.email == userLoginDto.email && _.password == encryptedPassword)
                .Include(_ => _.role)
                .FirstOrDefaultAsync();
        }
    }
}
