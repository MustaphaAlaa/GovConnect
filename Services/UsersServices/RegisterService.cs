﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using ModelDTO.API;
using ModelDTO.User;
using Models.Users;

namespace Services.UsersServices;

public class RegisterService
{
    private UserManager<User> _userManager;

    public RegisterService(UserManager<User> userManager)
    {
        _userManager = userManager;
    }


    public async Task<bool> ValidateRegisterAsync(RegisterDTO register, ApiResponse response, ModelStateDictionary modelState)
    {
        
        if (!modelState.IsValid)
        {
             response.statusCode = System.Net.HttpStatusCode.Conflict;
             response.IsSuccess = false;

            modelState.Values
                .SelectMany(v => v.Errors)
                .ToList().ForEach(v =>  response.ErrorMessages.Add(v.ErrorMessage));

            return false;
        }
        
        
        if (register == null)
        {
            response.statusCode = System.Net.HttpStatusCode.BadRequest;
            response.IsSuccess = false;
            response.ErrorMessages.Add("Cannot work without a register.");
            
            return false; 
        }
  
        var age = (int)((DateTime.Now - register.BirthDate).TotalDays) / 365;

        if (age < 18)
        {
            response.statusCode = System.Net.HttpStatusCode.BadRequest;
            response.IsSuccess = false;
            response.ErrorMessages.Add("Minimum age must be at least 18 years old.");

            return false; 
        }


        var UserExist = await _userManager.FindByEmailAsync(register.Email);

        if (UserExist != null)
        {
            response.statusCode = System.Net.HttpStatusCode.Conflict;
            response.IsSuccess = false;
            response.ErrorMessages.Add("Email Is Already Exist.");
            
            return false; 
        }

        return true; 
    }
}