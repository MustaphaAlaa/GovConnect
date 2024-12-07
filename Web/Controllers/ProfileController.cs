using System.Net;
using System.Security.Claims;
using System.Security.Principal;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ModelDTO.API;
using ModelDTO.User;
using Models.Users;

namespace Web.Controllers;

[Route("profile")]
[ApiController]
public class ProfileController : Controller
{
    private readonly UserManager<User> _userManager;
    private readonly IMapper _mapper;
    private readonly ApiResponse _response;

    public ProfileController(UserManager<User> userManager, IMapper mapper)
    {
        _userManager = userManager;
        _mapper = mapper;
        _response = new ApiResponse();
    }

    [HttpGet("{Id:guid}")]
    public async Task<IActionResult> profile(Guid Id)
    {
        if (Id == Guid.Empty)
        {
            _response.statusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("Not a valid id");
            return BadRequest(_response);
        }

        User? user = await _userManager.FindByIdAsync(Id.ToString());

        if (user == null)
        {
            _response.statusCode = HttpStatusCode.NotFound;
            _response.IsSuccess = true;
            _response.ErrorMessages.Add("User does not exist");
            return NotFound(_response);
        }
        var userDTO = _mapper.Map<UserDTO>(user);

        _response.statusCode = HttpStatusCode.OK;
        _response.IsSuccess = true;
        _response.Result = userDTO;
        return Ok(_response);
    }


    [Authorize()]
    [HttpPut("{Id:guid}")]
    public async Task<IActionResult> profile(UpdateUserDTO updateUserRequest)
    {
        if (updateUserRequest is null)
        {
            _response.statusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("objecet is null");
            return BadRequest(_response);
        }

        var userReqId = HttpContext.User.FindFirst(ClaimTypes.NameIdentifier);

        if (userReqId.Value != updateUserRequest.Id.ToString())
        {
            _response.statusCode = HttpStatusCode.Unauthorized;
            _response.IsSuccess = false;
            _response.ErrorMessages.Add("Unauthorized user");
            return Unauthorized(_response);
        }

        User? user = await _userManager.FindByIdAsync(updateUserRequest.ToString());

        if (user == null)
        {
            _response.statusCode = HttpStatusCode.NotFound;
            _response.IsSuccess = true;
            _response.ErrorMessages.Add("User does not exist");
            return NotFound(_response);
        }



        user.FirstName = updateUserRequest.FirstName;
        user.SecondName = updateUserRequest.SecondName;
        user.ThirdName = updateUserRequest.ThirdName;
        user.FourthName = updateUserRequest.FourthName;
        user.UserName = updateUserRequest.Username;
        user.Email = updateUserRequest.Email;
        user.NormalizedEmail = updateUserRequest.Email.ToUpper();
        user.PhoneNumber = updateUserRequest.PhoneNumber;
        user.Address = updateUserRequest.Address;
        user.ImagePath = updateUserRequest.ImagePath;

        var u = await _userManager.UpdateAsync(user);

        if (!u.Succeeded)
        {
            _response.statusCode = HttpStatusCode.BadRequest;
            _response.IsSuccess = false;

            foreach (var item in u.Errors)
            {
                _response.ErrorMessages.Add(item.Description);
            }

            return BadRequest(_response);
        }

        _response.statusCode = HttpStatusCode.OK;
        _response.IsSuccess = true;
        _response.ErrorMessages = null;
        var userMapped = _mapper.Map<UserDTO>(u);
        _response.Result = userMapped;

        return Ok(_response);



    }
}