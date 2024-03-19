using AutoMapper;
using Microservice.Client.ProjectAuth.JwtHandler;
using Microservice.Client.ProjectAuth.Models.DTOs;
using Microservice.Client.ProjectAuth.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Microservice.Client.ProjectAuth.Controllers
{
    [Route("auth")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly ResponseDTO _responseDTO;
        private readonly IJwtTokenGenerator _jwtTokenGenerator;
        public AuthController(IUserService userService, IMapper mapper,IJwtTokenGenerator jwtTokenGenerator)
        {
            _userService = userService;
            _mapper = mapper;
            _responseDTO = new ResponseDTO();
            _jwtTokenGenerator = jwtTokenGenerator;
        }

        [HttpPost]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] UserSignUpDTO userSignUpDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var res = await _userService.RegisterUser(userSignUpDTO);
            if (!res.Succeeded)
            {
                return Ok("User Already Exist!");
            }
            _responseDTO.IsSuccess = true;
            _responseDTO.Message = "User Created Successfully!";
                _responseDTO.Token = "";
            _responseDTO.User = res;
            return Ok(_responseDTO);
        }
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] UserSignInDTO userSignInDTO)
        {
            var user = await _userService.FindByEmailAsync(userSignInDTO.Email);
            if (user == null || !await _userService.CheckPasswordAsync(user, userSignInDTO.Password))
            {
                return NotFound("User not Found!");
            }
            IEnumerable<string> roles = await _userService.UserRoles(user);
            var token = _jwtTokenGenerator.GenerateToken(user, roles);
            //Token
            _responseDTO.IsSuccess = true;
            _responseDTO.Message = "Login Successfully!";
            _responseDTO.Token = token;
            _responseDTO.User = user;
            return Ok(_responseDTO);
        }
        [HttpPost]
        [Route("admin-register")]
        [Authorize("Admin")]
        public async Task<IActionResult> CreateAdmin([FromBody] UserSignUpDTO userSignUpDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var res = await _userService.CreateAdmin(userSignUpDTO);
            if(!res.Succeeded)
            {
                return Ok("Admin Already Exist!");
            }
            _responseDTO.IsSuccess = true;
            _responseDTO.Message = "AdminUser Created Successfully!";
            _responseDTO.Token = "";
            _responseDTO.User = res;
            return Ok(_responseDTO);
        }
    }
}
