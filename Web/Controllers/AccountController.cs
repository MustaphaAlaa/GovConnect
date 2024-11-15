using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModelDTO.API;
using ModelDTO.User;
using Models.Users;
using System.Text;

namespace Web.Controllers
{
    [AllowAnonymous]
    [Route("api/Accounts")]
    [ApiController]
    public class AccountsController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;
        private readonly ApiResponse _response;
        public AccountsController(UserManager<User> userManager, IMapper mapper)
        {
            _userManager = userManager;
            _mapper = mapper;
            _response = new ApiResponse();
        }

        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterDTO register)
        {

            /*
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

            if (register == null)
                throw new ArgumentNullException(nameof(register));

            var age = (int)((DateTime.Now - register.BirthDate).TotalDays) / 365;

            if (age < 18)
            {
                throw new ArgumentOutOfRangeException(nameof(age), $"is less than 18 years-old!");
            }

            if (!ModelState.IsValid)
            {
                throw new Exception("something wrong");
            }

            var UserExist = await _userManager.FindByEmailAsync(register.Email);

            if (UserExist != null)
            {

                _response.statusCode = System.Net.HttpStatusCode.Conflict;
                _response.IsSuccess = false;
                _response.ErrorMessages.Add("Email Is Already Exist.");

                return BadRequest(_response);
            }



            var user = _mapper.Map<User>(register);

            var result = await _userManager.CreateAsync(user, register.Password);
            if (result.Succeeded)
            {
                var UserDTO = _mapper.Map<UserDTO>(user);

                _response.statusCode = System.Net.HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = UserDTO;
                _response.ErrorMessages = null;
                return Ok(_response);
            }

            _response.statusCode = System.Net.HttpStatusCode.Forbidden;
            _response.IsSuccess = false;


            foreach (var err in result.Errors)
            {
                _response.ErrorMessages.Add(err.Description);
            }

            return BadRequest(_response);

        }

        [HttpPost("Login")]

        public Task<IActionResult> Login(LoginDTO login)
        {
            if (!ModelState.IsValid || login == null)
            {

            }
            return null;
        }
    }
}
