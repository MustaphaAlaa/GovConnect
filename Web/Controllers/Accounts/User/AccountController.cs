using AutoMapper;
using IServices.IApplicationServices.User;
using IServices.IUserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using ModelDTO.API;
using ModelDTO.Users;
using Models.Users;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;

namespace Web.Controllers.Accounts
{
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signManager;
        private readonly IMapper _mapper;
        private readonly IUserRegistrationService _userRegistrationService;
        private ApiResponse _response;
        private readonly IConfiguration _config;


        public AccountController(IConfiguration configuration,
            UserManager<User> userManager,
            IMapper mapper,
            SignInManager<User> signManager,
            IUserRegistrationService userRegistrationService)
        {
            _config = configuration;
            _userManager = userManager;
            _signManager = signManager;
            _mapper = mapper;
            _response = new ApiResponse();
            _userRegistrationService = userRegistrationService;

        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO register)
        {
            /*
             insted of typing the data everytime
             {
                "firstName": "mostafa",
                "lastName": "alaa",
                        "Username":"MostafaAlaa2050",
                "nationalNo": "846531",
                "gender": 0,
                "address": "wedfvb",
                "imagePath": "strinSDFASD",
                "email": "string@er.com",
                "confirmEmail": "string@er.com",
                "phoneNumber": "01125043780",
                "birthDate": "1990-11-30T05:26:08.325Z",
                "password": "mM@123456",
                "confirmPassword": "mM@123456",
                "countryId": 1
            }
             */
            bool valid = await _userRegistrationService.ValidateRegisterAsync(register, _response, ModelState);

            if (!valid)
            {
                return Ok(_response);
            }

            var user = _mapper.Map<User>(register);

            var result = await _userManager.CreateAsync(user, register.Password);

            if (result.Succeeded)
            {
                var UserDTO = _mapper.Map<UserDTO>(user);

                _response.StatusCode = System.Net.HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = UserDTO;
                _response.ErrorMessages = null;
                return Ok(_response);
            }

            _response.StatusCode = System.Net.HttpStatusCode.Forbidden;
            _response.IsSuccess = false;

            foreach (var err in result.Errors)
            {
                _response.ErrorMessages.Add(err.Description);
            }

            return BadRequest(_response);
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginDTO login)
        {

            var user = await _userManager.FindByEmailAsync(login.Email);

            if (user == null || !(await _userManager.CheckPasswordAsync(user, login.Password)))
            {
                this.UnAuthorizeResopnse(() => _response.ErrorMessages.Add("Password or Eamil invaild"));
            }

            var userRoles = await _userManager.GetRolesAsync(user);
            UserDTO userDto = _mapper.Map<UserDTO>(user);
            userDto.Roles = userRoles;
            var loginResponseDTO = new LoginResponseDTO
            {
                IsAuthorize = true,
                Email = login.Email,
                UserName = user?.UserName,
                Token = CreateToken(user),
                Roles = (List<string>)userDto.Roles
            };


            return OkResponse(loginResponseDTO);
        }


        private IActionResult UnAuthorizeResopnse(Action ErrorMsgs)
        {
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("Password or Eamil invaild");
            _response.StatusCode = HttpStatusCode.NonAuthoritativeInformation;
            _response.Result = null;
            return Unauthorized(_response);
        }

        private IActionResult OkResponse(object result, HttpStatusCode statusCode = HttpStatusCode.OK)
        {

            _response.IsSuccess = true;
            _response.ErrorMessages = null;
            _response.StatusCode = statusCode;
            _response.Result = result;
            return Ok(_response);
        }


        [HttpGet("auth")]
        [Authorize(policy: "JWT")]
        public IActionResult gojo()
        {

            var u = HttpContext.User;

            return Content("Auth works ;(" + $" {u.Identity?.IsAuthenticated}");
        }

        [HttpGet("adminEmp")]
        [Authorize(policy: "JWT", Roles = "Employee,Admin")]
        public IActionResult RoleTest()
        {

            var u = HttpContext.User;

            return Content("Auth works ;(" + $" {u.Identity?.IsAuthenticated}");
        }

        private string CreateToken(User user)
        {
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, user.UserName),
                new Claim(ClaimTypes.Email, user.Email),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["JWT:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var tokenDescriptor = new JwtSecurityToken(
                issuer: _config.GetValue<string>("JWT:is"),
                audience: _config.GetValue<string>("JWT:aud"),
                claims: claims,
                signingCredentials: creds,
                expires: DateTime.Now.AddMinutes(30)
            );

            string token = new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);
            return token;
        }

    }
}
