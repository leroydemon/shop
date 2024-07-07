using AutoMapper;
using BussinessLogicLevel.Interfaces;
using DbLevel.Filters;
using DbLevel.Interfaces;
using DbLevel.Models;
using DbLevel.Specifications;
using Infrastucture.DtoModels;

namespace BussinessLogicLevel.Services
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _userRepository;
        private readonly IMapper _mapper;
        public UserService(IRepository<User> userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }
        public async Task<IEnumerable<UserDto>> SearchAsync(UserFilter filter)
        {
            var spec = new UserSpecification(filter);
            var users = await _userRepository.ListAsync(spec);

            return _mapper.Map<List<UserDto>>(users);
        }

        public async Task SetOnlineAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            user.IsOnline = true;
            await _userRepository.SaveChangesAsync();
        }
        public async Task SetOfflineAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            user.IsOnline = false;
            await _userRepository.SaveChangesAsync();
        }

        public async Task RemoveAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            await _userRepository.DeleteAsync(user);
        }

        public async Task<UserDto> GetByIdAsync(Guid userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            return _mapper.Map<UserDto>(user);
        }

        public async Task<UserDto> UpdateAsync(UserDto user)
        {
            var updatedUser = await _userRepository.UpdateAsync(_mapper.Map<User>(user));

            return _mapper.Map<UserDto>(updatedUser);
        }

        public async Task<UserDto> AddAsync(UserDto userDto)
        {
            var user = _mapper.Map<User>(userDto);
            var addedUser = await _userRepository.AddAsync(user);

            return _mapper.Map<UserDto>(addedUser);
        }
    }
}

