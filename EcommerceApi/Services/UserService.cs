using AutoMapper;
using EcommerceApi.Helpers;
using EcommerceApi.Models.Database;
using EcommerceApi.Models.Dto;
using EcommerceApi.Repositories.Contracts;
using EcommerceApi.Services.Contracts;

namespace EcommerceApi.Services
{
    public class UserService : BaseService<User>, IUserService
    {
        IUserRepository _UserRepository;
        private CryptographyHelper _cryptographyHelper;
        private IAuthorizationService _authorizationService;
        private readonly IMapper _mapper;

        public UserService(IBaseRepository<User> repository, IConfiguration configuration, IAuthorizationService authorizationService, IMapper mapper) : base(repository) {
            _UserRepository = (IUserRepository)repository;
            _authorizationService = authorizationService;
            _mapper = mapper;
            _cryptographyHelper = new CryptographyHelper(configuration);
        }

        public async Task<IEnumerable<User>> GetByRole(Guid roleId)
        {
            return await _UserRepository.GetByRole(roleId);
        }

        public async Task<User> SaveUser(UserDto userDto)
        {
            User? existingUser = await _UserRepository.GetByEmail(userDto.email);
            if (existingUser != null) throw new Exception("User already exists");

            User user = _mapper.Map<User>(userDto);
            user.isActive = true;
            user.password = _cryptographyHelper.Encrypt(user.password);
            return await _UserRepository.Save(user);
        }

        public async Task<User?> DeleteUser(Guid id)
        {
            User? user = await GetById(id);
            if (user == null) return null;

            user.isActive = false;
            return await Update(user);
        }

        public async Task<UserLoggedDto?> Login(UserLoginDto userLoginDto)
        {
            var userLogged = await _UserRepository.Login(userLoginDto);
            if (userLogged == null) throw new Exception("Invalid email or password");

            UserLoggedDto userDto = _mapper.Map<User, UserLoggedDto>(userLogged);
            userDto.token = await _authorizationService.GenerateToken(userLogged);
            userDto.permissions = userLogged.role?.permissions.Select(_ => _.name).ToList();
            return userDto;
        }

        public async Task<User> ChangePassword(UserChangePasswordDto userChangePasswordDto)
        {
            User? user = await GetById(userChangePasswordDto.id);

            if (user == null) throw new Exception("User not found");

            if (!_cryptographyHelper.Compare(userChangePasswordDto.oldPassword, user.password)) throw new Exception("Invalid password");

            user.password = _cryptographyHelper.Encrypt(userChangePasswordDto.newPassword);
            return await Update(user);
        }

        public async Task<User?> GetByEmail(string email)
        {
            return await _UserRepository.GetByEmail(email);
        }

        public async Task<User?> GetIncludeRole(Guid id)
        {
            return await _UserRepository.GetIncludeRole(id);
        }

        public async Task<User> UpdateUser(UserDto userDto)
        {
            if (userDto.id == null) throw new Exception("User id is required");

            User? currentUser = await GetById(userDto.id ?? new Guid());
            if (currentUser == null) throw new Exception("User not found");

            User user = _mapper.Map<User>(userDto);
            user.password = currentUser.password;
            return await Update(user);
        }
    }
}
