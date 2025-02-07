using AutoMapper;
using IServices.IUserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModelDTO.API;
using ModelDTO.Users;
using Models.Users;

namespace Web.Controllers.Accounts
{
    [Route("api/account")]
    [ApiController]
    [AllowAnonymous]
    public class AccountController : ControllerBase
    {

        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signManager;
        private readonly IMapper _mapper;
        private readonly IUserRegistrationService _userRegistrationService;
        private readonly ApiResponse _response;

        public AccountController(UserManager<User> userManager, IMapper mapper, SignInManager<User> signManager, IUserRegistrationService userRegistrationService)
        {
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

        [HttpPost]
        public async Task<IActionResult> Login(object f){throw new NotImplementedException();}

    }
}
