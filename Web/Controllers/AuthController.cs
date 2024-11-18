using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModelDTO.API;
using ModelDTO.User;
using Models.Users;
using System.Text;
using Services.UsersServices;

namespace Web.Controllers;

[AllowAnonymous]
[Route("api/Accounts")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly UserManager<User> _userManager;
    private readonly SignInManager<User> _signManager;
    private readonly IMapper _mapper;
    private readonly RegisterService _registerService;
    private readonly ApiResponse _response;

    public AuthController(UserManager<User> userManager, IMapper mapper, SignInManager<User> signManager)
    {
        _userManager = userManager;
        _signManager = signManager;
        _mapper = mapper;
        _response = new ApiResponse();
        _registerService = new RegisterService(_userManager);
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
        bool valid = await _registerService.ValidateRegisterAsync(register, _response, ModelState);

        if (!valid)
        {
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
    public async Task<IActionResult> Login(LoginDTO login)
    {
        if (!ModelState.IsValid || login == null)
        {
            return BadRequest();
        }

        var result = await _signManager.PasswordSignInAsync(login.Email, login.Password, false, false);

        if (result.Succeeded)
        {
            /*   var user = await _userManager.FindByEmailAsync(login.Email);
        if (user != null)
        {
            var u = await _userManager.IsInRoleAsync(user, "user");
            _response.statusCode = System.Net.HttpStatusCode.OK;
            _response.IsSuccess = true;
            _response.Result = _mapper.Map<UserDTO>(user);
        }

        _response.statusCode = System.Net.HttpStatusCode.OK;
        _response.IsSuccess = true;
        _response.Result = _mapper.Map<UserDTO>(user);
*/
            return Ok();
        }


        return BadRequest(_response);
    }


    [HttpPost("Logout")]
    public async Task<IActionResult> Logout()
    {
        await _signManager.SignOutAsync();
        return Ok();
    }
}