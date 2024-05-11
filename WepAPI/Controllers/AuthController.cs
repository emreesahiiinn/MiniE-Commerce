using Business.Abstract;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WepAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : Controller
{
    [HttpPost("Login")]
    public ActionResult Login(UserForLoginDto userForLoginDto)
    {
        var userToLogin = authService.Login(userForLoginDto);
        if (!userToLogin.Success)
        {
            return BadRequest(userToLogin.Message);
        }

        var result = authService.CreateAccessToken(userToLogin.Data);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }

    [HttpPost("Register")]
    public ActionResult Register(UserForRegisterDto userForRegisterDto)
    {
        var userExists = authService.UserExists(userForRegisterDto.Email);
        if (!userExists.Success)
        {
            return BadRequest(userExists.Message);
        }

        var registerResult = authService.Register(userForRegisterDto, userForRegisterDto.Password);
        var result = authService.CreateAccessToken(registerResult.Data);
        if (!result.Success)
        {
            return BadRequest(result);
        }

        return Ok(result);
    }
}