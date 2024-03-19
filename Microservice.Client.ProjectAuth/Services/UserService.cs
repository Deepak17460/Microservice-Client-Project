
using AutoMapper;
using Microservice.Client.ProjectAuth.Domain;
using Microservice.Client.ProjectAuth.Models.DTOs;
using Microservice.Client.ProjectAuth.Repository;
using Microsoft.AspNetCore.Identity;

namespace Microservice.Client.ProjectAuth.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public UserService(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<IdentityResult> RegisterUser(UserSignUpDTO userSignUpDTO)
        {
            var userData = _mapper.Map<UserSignUpDTO,User>(userSignUpDTO);
            userData.UserName = userData.Email;
            var res = await _userRepository.RegisterUser(userData, userSignUpDTO.Password);
            return res;
        }
        public async Task<UserDTO> FindByEmailAsync(string email)
        {
            var user = await _userRepository.FindByEmail(email);
            return _mapper.Map<User, UserDTO>(user);
        }
        public async Task<Boolean> CheckPasswordAsync(UserDTO user, string password)
        {
            User userData = await _userRepository.FindByEmail(user.Email);
            return await _userRepository.CheckPasswordAsync(userData, password);
        }
        public async Task<IEnumerable<string>> UserRoles(UserDTO userDTO)
        {
            User user = await _userRepository.FindByEmail(userDTO.Email);
            return await _userRepository.UserRoles(user);
        }
        //Admin Creation
        public async Task<IdentityResult> CreateAdmin(UserSignUpDTO userSignUpDTO)
        {
            var userData = _mapper.Map<UserSignUpDTO, User>(userSignUpDTO);
            userData.UserName = userData.Email;
            var res = await _userRepository.CreateAdmin(userData, userSignUpDTO.Password);
            return res;
        }
    }
}
