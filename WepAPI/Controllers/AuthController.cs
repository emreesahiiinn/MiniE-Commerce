using Business.Abstract.Services;
using Entities.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WepAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController(IAuthService authService) : Controller
{
    [HttpPost("Login")]
    public async Task<ActionResult> Login(UserForLoginDto userForLoginDto)
    {
        var userToLogin = await authService.Login(userForLoginDto);
        if (!userToLogin.Status) return BadRequest(userToLogin.Message);

        var result = await authService.CreateAccessToken(userToLogin.Data);
        if (!result.Status) return BadRequest(result);

        return Ok(result);
    }

    [HttpPost("Register")]
    public async Task<ActionResult> Register(UserForRegisterDto userForRegisterDto)
    {
        var userExists = await authService.UserExists(userForRegisterDto.Email);
        if (!userExists.Status) return BadRequest(userExists.Message);

        var registerResult = await authService.Register(userForRegisterDto, userForRegisterDto.Password);
        var result = await authService.CreateAccessToken(registerResult.Data);
        if (!result.Status) return BadRequest(result);

        return Ok(result);
    }
}