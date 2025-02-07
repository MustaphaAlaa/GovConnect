using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModelDTO.API;
using ModelDTO.Users;
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
    private readonly UserRegistrationService _userRegistrationService;
    private readonly ApiResponse _response;

    public AuthController(UserManager<User> userManager, IMapper mapper, SignInManager<User> signManager)
    {
        _userManager = userManager;
        _signManager = signManager;
        _mapper = mapper;
        _response = new ApiResponse();
        _userRegistrationService = new UserRegistrationService(_userManager);
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
            var user = await _userManager.FindByEmailAsync(login.Email);
            if (user == null)
            {
                _response.ErrorMessages.Add("Email not found");
            }
            if (user != null)
            {
                var u = await _userManager.IsInRoleAsync(user, "user");
                _response.StatusCode = System.Net.HttpStatusCode.OK;
                _response.IsSuccess = true;
                _response.Result = _mapper.Map<UserDTO>(user);
            } 
            
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